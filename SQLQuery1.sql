select * from TblHareket

select 
	h.HareketId,
	u.UrunAd,
	(m.Ad + ' ' + m.Soyad) as 'Musteri',
	p.AdSoyad as 'Personel', 
	h.Fiyat  
from TblHareket h
inner join TblUrunler  u  on h.Urun = u.UrunId 
inner join TblMusteri  m  on h.Musteri = m.Id
inner join TblPersonel p  on h.Personel = p.Id