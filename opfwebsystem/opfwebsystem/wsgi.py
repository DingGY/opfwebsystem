"""
WSGI config for opfwebsystem project.

It exposes the WSGI callable as a module-level variable named ``application``.

For more information on this file, see
https://docs.djangoproject.com/en/2.0/howto/deployment/wsgi/
"""

import os
import sys

from django.core.wsgi import get_wsgi_application
path = '/opt/django/opfwebsystem'
if path not in sys.path:
    sys.path.append('/opt/django/opfwebsystem')
    sys.path.append('/opt/django/opfwebsystem/opfwebsystem')
os.environ.setdefault("DJANGO_SETTINGS_MODULE", "opfwebsystem.settings")

application = get_wsgi_application()
