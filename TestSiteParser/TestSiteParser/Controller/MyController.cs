using System;
using TestSiteParser.Model;
using TestSiteParser.Model.Interface;
using TestSiteParser.Parser;
using TestSiteParser.Parser.Interface;
using TestSiteParser.Utilities;
using TestSiteParser.View;
using TestSiteParser.View.Interface;

namespace TestSiteParser.Controller
{
    internal sealed class MyController
    {
        private static readonly string Url = "http://www.rts-tender.ru/auctionsearch";

        private IParser _parser;
        private IModel _model;
        private IView _view;

        public void Init()
        {
            InitParser();
            InitModel();
            InitView();
        }

        public void Start()
        {
            MyLogger.Log(_model.Data);
            _view.Show();
        }

        private void InitParser()
        {
            _parser = new MySiteParser(Url);
        }

        private void InitModel()
        {
            var data = _parser.Parse();
            _model = new MyModel(data);
        }

        private void InitView()
        {
            _view = new MyView(_model);
        }
    }
}
