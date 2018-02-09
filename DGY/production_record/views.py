from django.shortcuts import render
from .models import *
from django.shortcuts import render_to_response
from django.http import *
from django.views.generic import ListView
from django.views.decorators.csrf import *
from django.contrib.auth.decorators import *
# Create your views here.


class MeterCodeList(ListView):
    model = MeterCode
    template_name = "metercode_list.html"
    paginate_by = 5
    ordering = "-product_date"
    def get_context_data(self, **kwargs):
        context = super(MeterCodeList,self).get_context_data(**kwargs)
        context['count'] = 0
        return context

@csrf_exempt
def save_production_info(request):
    info_list = request.POST['data'].split("::")
    print(info_list)
    meter = MeterCode(
        asset_management_coding = info_list[0],
        worker_addr = request.META['REMOTE_ADDR'],
        product_status = info_list[1],
    )
    meter.save()
    return HttpResponse("ok")



class MeterCodeFindList(ListView):
    model = MeterCode
    template_name = "metercode_list.html"
    paginate_by = 5
    ordering = "-product_date"
    def get_context_data(self, **kwargs):
        context = super(MeterCodeFindList,self).get_context_data(**kwargs)
        context['count'] = 0
        return context
    def get(self, request, *args, **kwargs):
        self.queryset = MeterCode.objects.using('production-record')\
            .filter(asset_management_coding=self.request.GET['code'])
        return super(MeterCodeFindList,self).get(request, *args, **kwargs)
        