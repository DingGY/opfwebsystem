# -*- coding: utf-8 -*-
# Generated by Django 1.11.6 on 2017-12-11 07:47
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('bug_manage', '0005_auto_20171211_1543'),
    ]

    operations = [
        migrations.AddField(
            model_name='stepaction',
            name='id',
            field=models.AutoField(auto_created=True, default=1, primary_key=True, serialize=False, verbose_name='ID'),
            preserve_default=False,
        ),
        migrations.AlterField(
            model_name='stepaction',
            name='num',
            field=models.IntegerField(blank=True, null=True),
        ),
    ]
