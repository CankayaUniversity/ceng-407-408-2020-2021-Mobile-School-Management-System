using Microsoft.AspNetCore.Mvc.Rendering;
using MobilOkulProc.Entities.General;

using X.PagedList;

namespace MobilOkulProc.MobileApp.Models
{
    public class MessagePageModel<T> where T : class, new()
    {
        public Mesajlar<T> Mesajlar { get; set; }
        public IPagedList<T> PagedList { get; set; }
        public SelectList SenderList { get; set; }
        public int SenderId { get; set; }
        public SelectList ReceiverList { get; set; }
        public int ReceiverId { get; set; }
        public SelectList StudentList { get; set; }
    }
}
