# -*- coding: utf-8 -*-
# Generated by Django 1.11.6 on 2017-11-06 06:11
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('bug_manage', '0001_initial'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='serialdata',
            name='id',
        ),
        migrations.AlterField(
            model_name='serialdata',
            name='create_date',
            field=models.DateTimeField(auto_now_add=True, primary_key=True, serialize=False),
        ),
    ]
