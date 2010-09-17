using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace CodeValue.CodeLight.Client.Foundation
{

    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static string GetPropertyNameFromExpression<T>(Expression<Func<T>> property)
        {
            var lambda = (LambdaExpression)property;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member.Name;
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> property)
        {
            OnPropertyChanged(GetPropertyNameFromExpression(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
    public abstract class ViewModelBase : NotifyPropertyChanged
    {
        private object _model;

        public virtual object Model
        {
            get { return _model; }
            set
            {
                _model = value;
                ModelChanged();
            }
        }

        protected abstract void ModelChanged();

     

    }

    public class ViewModelBase<T> : ViewModelBase
    {
        public new virtual T Model
        {
            get { return (T)base.Model; }
            set { base.Model = value; }
        }

        protected override void ModelChanged()
        {
            OnPropertyChanged(() => Model);
        }
    }
}
