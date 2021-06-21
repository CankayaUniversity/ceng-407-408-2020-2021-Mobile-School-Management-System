using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.General;

namespace MobilOkulProc.WebUserApp.ViewModels
{
    public class ExamViewModel<T> where T : class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
        public SelectList List { get; set; }
        public int SelectedId { get; set; }
        public SelectList List2 { get; set; }
        public int SelectedId2 { get; set; }

    }
}
