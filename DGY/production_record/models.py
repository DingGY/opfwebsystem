from django.db import models

# Create your models here.
class MeterCode(models.Model):
    asset_management_coding = models.CharField(max_length=30)
    worker_addr = models.GenericIPAddressField(blank=True, null=True)
    product_status = models.CharField(max_length=30)
    product_date = models.DateTimeField(\
        auto_now_add=True,blank=True)

    def __str__(self):
        return self.asset_management_coding