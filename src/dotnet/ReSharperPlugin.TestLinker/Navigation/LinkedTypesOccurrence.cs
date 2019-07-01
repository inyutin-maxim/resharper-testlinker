// Copyright Matthias Koch 2017.
// Distributed under the MIT License.
// https://github.com/matkoch/Nuke/blob/master/LICENSE

using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Occurrences;
using JetBrains.ReSharper.Psi;

namespace ReSharperPlugin.TestLinker.Navigation
{
    public class LinkedTypesOccurrence : DeclaredElementOccurrence
    {
        public LinkedTypesOccurrence ([NotNull] IDeclaredElement element, OccurrenceType occurrenceKind)
            : base(element, occurrenceKind)
        {
        }
    }
}
