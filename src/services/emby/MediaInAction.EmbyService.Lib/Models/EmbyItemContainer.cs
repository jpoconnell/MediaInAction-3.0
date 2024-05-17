using System.Collections.Generic;

namespace MediaInAction.EmbyService.Models
{
    public class EmbyItemContainer<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecordCount { get; set; }
    }
}