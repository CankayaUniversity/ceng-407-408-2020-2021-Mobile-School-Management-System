using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.General;
using X.PagedList;

namespace MobilOkulProc.WebApp.ViewModels
{
    public class AbsenceViewModel<T> where T : class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
        public IPagedList<T> PagedList { get; set; }
        public SelectList List { get; set; }
        public int SelectedId { get; set; }

    }
}
