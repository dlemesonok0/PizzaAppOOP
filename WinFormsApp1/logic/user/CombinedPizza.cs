// namespace WinFormsApp1.logic.user;
//
// public class CombinedPizza : Pizza
// {
//     public List<Pizza> Parts { get; }
//
//     public IEnumerable<Ingredient> Ingredients => 
//         Parts.SelectMany(p => p.Ingredients);
//
//     public decimal Cost => 
//         Parts.Sum(p => p.Pizza.Cost * p.Ratio);
//
//     public CombinedPizza(IEnumerable<(IPizza Pizza, decimal Ratio)> parts)
//     {
//         Parts = new List<(IPizza Pizza, decimal Ratio)>(parts);
//
//         // Проверяем, что все части используют одну основу
//         var baseNames = Parts.Select(p => p.Pizza.Base.Name).Distinct().ToList();
//         if (baseNames.Count > 1)
//             throw new InvalidOperationException("Все части комбинированной пиццы должны использовать одну и ту же основу");
//
//         // Проверяем, что сумма долей равна 100%
//         var totalRatio = Parts.Sum(p => p.Ratio);
//         if (Math.Abs(totalRatio - 1.0m) > 0.01m)
//             throw new ArgumentException("Сумма всех долей должна быть равна 1.0 (100%)");
//
//         // Устанавливаем общее имя
//         Name = string.Join(" + ", Parts.Select(p => $"{p.Pizza.Name} ({p.Ratio:P0})"));
//         Base = Parts[0].Pizza.Base;
//     }
// }