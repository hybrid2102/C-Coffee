namespace C_Coffee.Extensions
{
    public static class ListExtensions
    {
        public static T? NextItem<T>(this List<T> list, T? currentItem)
        {
            if (list == null || currentItem is null) return default;

            var currentIndex = list.IndexOf(currentItem);
            return currentIndex switch
            {
                // L'elemento corrente non è nella lista
                -1 => default,

                // Se l'elemento corrente è l'ultimo, ritorna il primo
                var index when index == list.Count - 1 => list[0],

                // Altrimenti, ritorna l'elemento successivo
                _ => list[currentIndex + 1], 
            };
        }
    }
}