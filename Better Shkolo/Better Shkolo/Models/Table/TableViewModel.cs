﻿namespace BetterShkolo.Models.Table
{
    public class TableViewModel
    {
        public int GradeId { get; set; }
        public List<TableDisplayModel> Tables { get; set; } = null!;
    }
}
