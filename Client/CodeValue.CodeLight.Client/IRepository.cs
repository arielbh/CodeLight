using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq.Expressions;

namespace CodeValue.CodeLight.Client
{
    /// <summary>
    /// Interface for Client Repository
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface IRepository<T>
    {

        /// <summary>
        /// Asynchronously Gets All entities of T. Calls the specified callback when result returned.
        /// </summary>
        /// <param name="callback">Callback for result.</param>
        void All(Action<IEnumerable<T>>  callback);

        /// <summary>
        /// Asynchronously Finds All entities of T. Calls the specified callback when result returned.
        /// </summary>
        /// <param name="filter">The filter to apply on T.</param>
        /// <param name="callback">Callback for result.</param>
        void FindAll(Expression<Func<T, bool>> filter, Action<IEnumerable<T>> callback);

    }
}
