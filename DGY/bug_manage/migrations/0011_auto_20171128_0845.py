# -*- coding: utf-8 -*-
# Generated by Django 1.11.6 on 2017-11-28 00:45
from __future__ import unicode_literals

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('bug_manage', '0010_auto_20171127_1720'),
    ]

    operations = [
        migrations.RenameModel(
            old_name='Bug',
            new_name='Task',
        ),
    ]
