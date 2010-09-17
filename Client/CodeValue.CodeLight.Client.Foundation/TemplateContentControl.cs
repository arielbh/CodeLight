using System;
using System.Windows;
using System.Windows.Controls;

namespace CodeValue.CodeLight.Client.Foundation
{
    public class TemplateContentControl : ContentControl
    {
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            if (newContent is UIElement) return;

            string key = newContent.GetType().Name;
            if (KeyLocator != null)
            {
                key = KeyLocator(newContent);
            }
            if (TemplateLocator != null)
            {
                ContentTemplate = TemplateLocator(newContent, key);
            }
            else
                ContentTemplate = (DataTemplate)Application.Current.Resources[key];
        }

        public Func<object, string> KeyLocator
        {
            get { return (Func<object, string>)GetValue(KeyLocatorProperty); }
            set { SetValue(KeyLocatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyLocator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyLocatorProperty =
            DependencyProperty.Register("KeyLocator", typeof(Func<object, string>), typeof(TemplateContentControl), new PropertyMetadata(null));

        public Func<object, string, DataTemplate> TemplateLocator
        {
            get { return (Func<object, string, DataTemplate>)GetValue(TemplateLocatorProperty); }
            set { SetValue(TemplateLocatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TemplateLocator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemplateLocatorProperty =
            DependencyProperty.Register("TemplateLocator", typeof(Func<object, string, DataTemplate>), typeof(TemplateContentControl), new PropertyMetadata(null));


    }
}
