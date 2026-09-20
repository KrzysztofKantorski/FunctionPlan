using Dapper;

namespace Application.Common.Helpers
{
    public static class SqlExtensions
    {


        //Set date range
        public static void AddDateRangeFilter(
            this List<string> conditions,
            DynamicParameters parameters,
            string columnName,
            DateTime? startDate,
            DateTime? endDate
        )
        {
            if (startDate.HasValue)
            {
                conditions.Add($"{columnName} >= @StartDate");
                parameters.Add("StartDate", startDate.Value);
            }

            if (endDate.HasValue)
            {
                conditions.Add($"{columnName} >= @StartDate");
                parameters.Add("EndDate", endDate.Value.Date.AddDays(1));
            }
        }



        //Add sorting order
        public static void ApplySorting(string sql, string columnName, string? sortOrder)
        {
            var sortDirection = sortOrder?.ToLower() == "desc" ? "DESC" : "ASC";
            sql += $"\nORDER BY {columnName} {sortDirection}";
        }


        //Apply search term LIKE condition
        public static void ApplySearchTerm(string sql, string columnName, string? searchTerm, 
            List<string> conditions, DynamicParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                conditions.Add($"{columnName} ILIKE @SearchTerm");
                parameters.Add("SearchTerm", $"%{searchTerm}%");
            }
        }


        //Apply where conditions
        public static string ApplyWhereConditions(string sql, List<string> conditions)
        {
            if (!conditions.Any()) return sql;
            return $"{sql}\nWHERE " + string.Join(" AND ", conditions);
        }
    }
}
