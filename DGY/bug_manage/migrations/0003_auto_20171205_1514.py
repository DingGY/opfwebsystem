# -*- coding: utf-8 -*-
# Generated by Django 1.11.6 on 2017-12-05 07:14
from __future__ import unicode_literals

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('bug_manage', '0002_auto_20171205_1454'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='logic',
            name='frame_set',
        ),
        migrations.AlterField(
            model_name='logic',
            name='func_id',
            field=models.ForeignKey(null=True, on_delete=django.db.models.deletion.CASCADE, to='bug_manage.FuncMessage'),
        ),
    ]