using System.Linq;
using JetBrains.Application.DataContext;
using JetBrains.Diagnostics;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Feature.Services.Navigation;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.DataContext;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Caches2;
using JetBrains.TextControl;
using JetBrains.TextControl.DataContext;
using JetBrains.Util;
using ReSharperPlugin.TestLinker.Utils;

namespace ReSharperPlugin.TestLinker.Navigation
{
    public abstract class LinkedTypesContextSearchBase : IContextSearch
    {
        public bool IsAvailable(IDataContext dataContext)
        {
            return true;
        }

        public bool IsContextApplicable(IDataContext dataContext)
        {
            return ContextNavigationUtil.CheckDefaultApplicability<CSharpLanguage>(dataContext);
        }

        public LinkedTypesSearchRequest CreateSearchRequest(IDataContext dataContext)
        {
            var typesFromTextControlService = dataContext.GetComponent<ITypesFromTextControlService>().NotNull();
            var textControl = dataContext.GetData(TextControlDataConstants.TEXT_CONTROL);
            var solution = dataContext.GetData(ProjectModelDataConstants.SOLUTION);

            var declaredElements = dataContext.GetData(PsiDataConstants.DECLARED_ELEMENTS_FROM_ALL_CONTEXTS);
            // TODO: static classes appear twice
            var type = declaredElements?.OfType<ClassLikeTypeElement>().Distinct(x => x.ToString()).SingleOrFirstOrDefaultErr()
                       ?? typesFromTextControlService.GetTypesFromCaretOrFile(textControl.NotNull(), solution.NotNull()).SingleOrFirstOrDefaultErr();

            return type != null ? CreateSearchRequest(type, textControl) : null;
        }

        protected abstract LinkedTypesSearchRequest CreateSearchRequest(ITypeElement type, ITextControl textControl);
    }
}