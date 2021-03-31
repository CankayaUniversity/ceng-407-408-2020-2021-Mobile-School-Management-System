using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MobilOkulProc.Entities.General;

namespace MobilOkulProc.WebAPI.Data
{

    //Dinamik tüm entityler için cruds işlemleri yapacak genel bir şablon
    public interface IEntityRepository<T> where T:class, new()
    {
        int Get_KayitID(EntityEntry<T> _entry, T ent);
        Mesajlar<T> Listele(Expression<Func<T, bool>> filtre = null); //List<School> 
        Mesajlar<T> Getir(Expression<Func<T, bool>> filtre = null); // School
        Mesajlar<T> Ekle(T ent); //1- Başarılı oldumu?, 2- İşlem sonuç mesajı ne?, 3-KayitID?
        Mesajlar<T> Duzelt(T ent);
        Mesajlar<T> Sil(T ent);

    }
}
