# -*- coding: utf-8 -*-
# Generated by Django 1.11.6 on 2017-11-07 02:31
from __future__ import unicode_literals

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('bug_manage', '0002_auto_20171106_1411'),
    ]

    operations = [
        migrations.RenameModel(
            old_name='User',
            new_name='ClientUser',
        ),
    ]
