using System;
using TestSiteParser.Model.Interface;
using TestSiteParser.View.Interface;

namespace TestSiteParser.View
{
    internal sealed class MyView : IView
    {
        private readonly IModel _model;

        public MyView(IModel model)
        {
            _model = model;
        }

        public void Show()
        {
            Console.Write(_model.Data);
        }
    }
}
