"""DGY URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/1.11/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  url(r'^$', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  url(r'^$', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.conf.urls import url, include
    2. Add a URL to urlpatterns:  url(r'^blog/', include('blog.urls'))
"""
from django.conf.urls import url, include
from django.contrib import admin
from bug_manage.views import *
urlpatterns = [
    url(r'^admin/', admin.site.urls),
    url(r'^test/$', test),
    url(r'^login/$', login_view),
    url(r'^login/check/$', login_check),
    url(r'^websocket/webcomm/$', web_comm),
    url(r'^bug_manage/index/$', boot),
    url(r'^bug_manage/manage/$', bug_manage),
    url(r'^localclient/$', local_client),
    url(r'^ajax/set_config/$', set_config),
    url(r'^ajax/step/(.+)/$', step_action),
    url(r'^ajax/task/(.+)/$', task_action),
    url(r'^ajax/func/(.+)/$', func_action),
    url(r'^ajax/index/get_task/$', get_task_info),
]
