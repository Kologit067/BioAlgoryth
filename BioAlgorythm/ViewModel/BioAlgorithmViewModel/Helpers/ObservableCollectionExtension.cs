using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BioAlgorithmViewModel.Helpers
{
    public static class ObservableCollectionExtension
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> input)
        {
            ObservableCollection<T> result = new ObservableCollection<T>();
            foreach (var item in input)
            {
                result.Add(item);
            }
            return result;
        }

    }
}
