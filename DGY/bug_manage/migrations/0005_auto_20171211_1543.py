# -*- coding: utf-8 -*-
# Generated by Django 1.11.6 on 2017-12-11 07:43
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('bug_manage', '0004_auto_20171205_1525'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='stepaction',
            name='id',
        ),
        migrations.AlterField(
            model_name='stepaction',
            name='num',
            field=models.IntegerField(primary_key=True, serialize=False),
        ),
    ]