// Copyright Matthias Koch 2017.
// Distributed under the MIT License.
// https://github.com/matkoch/Nuke/blob/master/LICENSE

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Application;
using JetBrains.Application.UI.TreeModels;
using JetBrains.ReSharper.Feature.Services.Tree;
using JetBrains.ReSharper.Feature.Services.Tree.SectionsManagement;
using JetBrains.Util;

namespace ReSharperPlugin.TestLinker.Navigation
{
    [ShellFeaturePart]
    public class LinkedTypesOccurrenceSectionProvider : OccurrenceSectionProvider
    {
        public override bool IsApplicable ([NotNull] IOccurrenceBrowserDescriptor descriptor)
        {
            return descriptor is LinkedTypesOccurrenceBrowserDescriptor;
        }

        public override ICollection<TreeSection> GetTreeSections ([NotNull] IOccurrenceBrowserDescriptor descriptor)
        {
            return descriptor.OccurrenceSections.Select(
                        x =>
                        {
                            var caption = $"LinkedTypesOccurrenceSectionProvider: Found {x.Items.Count} linked {NounUtil.ToPluralOrSingular("type", x.Items.Count)}";
                            return new TreeSection(x.Model, caption);
                        })
                    .ToList();
        }
    }
}
