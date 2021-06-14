using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MobilOkulProc.Entities.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Data
{
    public class EfEntityRepository<TEntity, TContext>:IEntityRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {
        public Mesajlar<TEntity> Duzelt(TEntity ent)
        {
            Mesajlar<TEntity> mesajlar = new Mesajlar<TEntity>();

            try
            {
                PropertyInfo durum = ent.GetType().GetProperty("Status");
                if (durum != null)
                {
                    durum.SetValue(ent, true);
                }

                using (var cnt = new TContext())
                {
                    var obj = cnt.Entry(ent);

                    IEnumerable<PropertyEntry> list = cnt.Entry(ent).Properties;
                    string pKey = Get_Pk_Name(obj);

                    foreach (var item in list)
                    {
                        if (item.CurrentValue != null && item.Metadata.Name != pKey)
                        {
                            item.IsModified = true;
                        }
                    }

                    cnt.SaveChanges();

                    mesajlar.KayitID = Get_KayitID(obj, ent);
                }
                mesajlar.Durum = true;
                mesajlar.Mesaj = "Bilgiler güncellendi.";
                mesajlar.Status = "success";
            }
            catch (Exception ex)
            {
                mesajlar.Durum = false;
                mesajlar.Mesaj = ex.Message + Environment.NewLine + ex.InnerException;
                mesajlar.Status = "danger";

            }

            return mesajlar;
        }

        public string Get_Pk_Name(EntityEntry<TEntity> _entry)
        {
            return _entry.Metadata.FindPrimaryKey().Properties.Select(x => x.Name).Single();
        }

        public Mesajlar<TEntity> Ekle(TEntity ent)
        {
            Mesajlar<TEntity> mesajlar = new Mesajlar<TEntity>();

            try
            {
                PropertyInfo durum = ent.GetType().GetProperty("Status");
                if (durum != null)
                {
                    durum.SetValue(ent, true);
                }

                using (var cnt = new TContext())
                {
                    var obj = cnt.Entry(ent);
                    obj.State = EntityState.Added;
                    cnt.SaveChanges();

                    mesajlar.KayitID = Get_KayitID(obj, ent);
                }

                mesajlar.Durum = true;
                mesajlar.Mesaj = "Bilgiler Kaydedildi.";
                mesajlar.Status = "success";
            }
            catch (Exception ex)
            {
                mesajlar.Durum = false;
                mesajlar.Mesaj = ex.Message + Environment.NewLine + ex.InnerException;
                mesajlar.Status = "danger";
               
            }

            return mesajlar;
        }

        public Mesajlar<TEntity> Getir(Expression<Func<TEntity, bool>> filtre = null)
        {
            Mesajlar<TEntity> mesajlar = new Mesajlar<TEntity>();

            try
            {
                using (var cnt = new TContext())
                {
                    var obj = cnt.Set<TEntity>();
                    mesajlar.Nesne = obj.SingleOrDefault(filtre);
                }

                mesajlar.Durum = true;
                mesajlar.Mesaj = "Bilgiler getirildi.";
                mesajlar.Status = "success";
            }
            catch (Exception ex)
            {
                mesajlar.Durum = false;
                mesajlar.Mesaj = ex.Message + Environment.NewLine + ex.InnerException;
                mesajlar.Status = "danger";

            }

            return mesajlar;
        }

        public Mesajlar<TEntity> Getir_Iliskisel(Expression<Func<TEntity, bool>> filtre = null)
        {
            Mesajlar<TEntity> mesajlar = new Mesajlar<TEntity>();

            try
            {
                using (var cnt = new TContext())
                {
                    IQueryable<TEntity> obj = cnt.Set<TEntity>();

                    Type tip = typeof(TEntity);

                    PropertyInfo[] pi = tip.GetProperties();

                    foreach (var i in pi)
                    {
                        ForeignKeyAttribute fk = i.GetCustomAttribute<ForeignKeyAttribute>();

                        if (fk != null)
                        {
                            obj = obj.Include(fk.Name);
                        }
                    }

                    mesajlar.Nesne = obj.SingleOrDefault(filtre);
                }

                mesajlar.Durum = true;
                mesajlar.Mesaj = "Bilgiler getirildi.";
                mesajlar.Status = "success";
            }
            catch (Exception ex)
            {
                mesajlar.Durum = false;
                mesajlar.Mesaj = ex.Message + Environment.NewLine + ex.InnerException;

            }

            return mesajlar;
        }
        public Mesajlar<TEntity> Getir_ListeIliskisel(Expression<Func<TEntity, bool>> filtre = null)
        {
            Mesajlar<TEntity> mesajlar = new Mesajlar<TEntity>();

            try
            {
                using (var cnt = new TContext())
                {
                    IQueryable<TEntity> obj = cnt.Set<TEntity>();

                    Type tip = typeof(TEntity);

                    PropertyInfo[] pi = tip.GetProperties();

                    foreach (var i in pi)
                    {
                        ForeignKeyAttribute fk = i.GetCustomAttribute<ForeignKeyAttribute>();

                        if (fk != null)
                        {
                            obj = obj.Include(fk.Name);
                        }
                    }

                    mesajlar.Liste = obj.Where(filtre).ToList();
                }

                mesajlar.Durum = true;
                mesajlar.Mesaj = "Bilgiler getirildi.";
                mesajlar.Status = "success";
            }
            catch (Exception ex)
            {
                mesajlar.Durum = false;
                mesajlar.Mesaj = ex.Message + Environment.NewLine + ex.InnerException;

            }

            return mesajlar;
        }

        public int Get_KayitID(EntityEntry<TEntity> _entry, TEntity ent)
        {
            string msj = "";
            int ID = 0;

            try
            {
                var idName = _entry.Metadata.FindPrimaryKey().Properties.Select(x => x.Name).Single();
                ID = (int)ent.GetType().GetProperty(idName).GetValue(ent);
            }
            catch (Exception ex)
            {
                msj = ex.Message; 
            }

            return ID;
        }

        public Mesajlar<TEntity> Listele(Expression<Func<TEntity, bool>> filtre = null)
        {
            Mesajlar<TEntity> m = new Mesajlar<TEntity>();

            try
            {
                using (var cnt = new TContext())
                {
                    var entryNesne = cnt.Set<TEntity>();

                    if (filtre == null)
                    {
                       m.Liste = entryNesne.ToList();
                    }
                    else
                    {

                        m.Liste = entryNesne.Where(filtre).ToList();
                    }

                }

                m.Durum = true;
                m.Mesaj = "Kayıt bilgileri listelendi.";
                m.Status = "success";
            }
            catch (Exception ex)
            {
                m.Durum = false;
                m.Mesaj = ex.Message + Environment.NewLine + ex.StackTrace;
            }

            return m;
        }

        public Mesajlar<TEntity> Sil(TEntity ent)
        {
            Mesajlar<TEntity> mesajlar = new Mesajlar<TEntity>();

            try
            {
                PropertyInfo durum = ent.GetType().GetProperty("Status");
                if (durum != null)
                {
                    durum.SetValue(ent, false);
                }

                using(var cnt = new TContext())
                {
                    var obj = cnt.Entry(ent);
                    obj.State = EntityState.Modified;
                    cnt.SaveChanges();
                }

                mesajlar.Durum = true;
                mesajlar.Mesaj = "Bilgiler silindi";
                mesajlar.Status = "success";
            }
            catch (Exception ex)
            {
                mesajlar.Durum = false;
                mesajlar.Mesaj = ex.Message + Environment.NewLine + ex.InnerException;
                mesajlar.Status = "danger";
            }

            return mesajlar;
        }
    }
}
