from django.db import models
from django.utils import timezone


# Create your models here.
class Meter(models.Model):
    address = models.CharField(max_length=12)
    bar_code = models.CharField(max_length=64, blank=True)
    product_date = models.DateTimeField(\
        auto_now_add=True,blank=True)
    last_use_date = models.DateTimeField(\
        auto_now_add=True,blank=True)
    first_use_date = models.DateTimeField(\
        auto_now_add=True,blank=True)

    def __str__(self):
        return self.address


class SerialData(models.Model):
    create_date = models.DateTimeField(\
        auto_now_add=True)
    addr = models.CharField(max_length=12, blank=True, null=True)
    status_word = models.CharField(max_length=10, blank=True, null=True)
    data_len = models.CharField(max_length=10, blank=True, null=True)
    data = models.CharField(max_length=200, blank=True, null=True)
    cs = models.CharField(max_length=10, blank=True, null=True)
    def __str__(self):
        return self.create_date.__str__()


class ClientUser(models.Model):
    ip4_addr = models.GenericIPAddressField(blank=True, null=True)
    com_id = models.CharField(max_length=5, blank=True)
    user_id = models.CharField(max_length=30, null=True)
    send_status = models.BooleanField(default=False)
    get_status = models.BooleanField(default=False)
    get_data = models.CharField(max_length=200, blank=True, null=True)
    send_data = models.CharField(max_length=200, blank=True, null=True)
    user_data = models.ForeignKey(\
        "SerialData", null=True,on_delete=models.CASCADE)
    user_meter = models.ForeignKey(\
        "Meter",null=True,on_delete=models.CASCADE)

    @classmethod
    def create(cls, client_data):
        meter = Meter(
            address=client_data.meter_addr, last_use_date=timezone.now())
        meter.save()
        serial_data = SerialData(
            addr=client_data.meter_addr,
            cs=client_data.meter_cs,
            status_word=client_data.meter_status,
            data_len=client_data.meter_data_len,
            data=client_data.meter_data,
            create_date=timezone.now())
        serial_data.save()
        user = cls(
            ip4_addr=client_data.client_ipv4,
            com_id=client_data.client_comid,
            user_id=client_data.client_user_id,
            send_status=False,
            send_data="",
            user_data=serial_data,
            user_meter=meter)
        return user

    def __str__(self):
        return u"%s->%s" % (self.ip4_addr, self.com_id)


class Logic(models.Model):
    name = models.CharField(max_length=50,blank=True,null=True)
    address = models.CharField(max_length=12)
    ischange_addr = models.BooleanField(default=False)
    isFE_begin = models.BooleanField(default=False)
    send_delay = models.IntegerField(blank=True,null=True)
    read_delay = models.IntegerField(blank=True,null=True)
    frame = models.CharField(max_length=500,blank=True,null=True)
    func_id = models.CharField(max_length=100,blank=True,null=True)
    display_msg = models.CharField(max_length=200,blank=True,null=True)
    val0 = models.CharField(max_length=100,blank=True,null=True)
    val1 = models.CharField(max_length=100,blank=True,null=True)
    val2 = models.CharField(max_length=100,blank=True,null=True)
    val3 = models.CharField(max_length=100,blank=True,null=True)
    val4 = models.CharField(max_length=100,blank=True,null=True)
    val5 = models.CharField(max_length=100,blank=True,null=True)
    val6 = models.CharField(max_length=100,blank=True,null=True)
    val7 = models.CharField(max_length=100,blank=True,null=True)
    val8 = models.CharField(max_length=100,blank=True,null=True)
    val9 = models.CharField(max_length=100,blank=True,null=True)
    def __str__(self):
        return u"%s" % self.name


class StepAction(models.Model):
    num = models.IntegerField(blank=True,null=True)
    act = models.ForeignKey(Logic, null=True)

class FuncMessage(models.Model):
    name = models.CharField(max_length=200)
    create_date = models.DateTimeField(
        auto_now_add=True)
    func_id = models.CharField(max_length=200,blank=True,null=True)
    msg = models.CharField(max_length=500)
    def __str__(self):
        return u"%s" % self.name

class Task(models.Model):
    create_date = models.DateTimeField(
        auto_now_add=True)
    founder = models.CharField(max_length=50)
    name = models.CharField(max_length=200)
    msg = models.CharField(max_length=500,blank=True,null=True)
    step = models.ManyToManyField(StepAction,related_name="step")
    def delete(self):
        '''delete every step for the task'''
        step_list = self.step.all()
        for step in step_list:
            step.delete()
        return super(Task,self).delete()
    def __str__(self):
        return u"%s" % self.name