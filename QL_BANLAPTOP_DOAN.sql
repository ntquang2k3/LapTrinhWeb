Use master
go
CREATE DATABASE LTW_BANLAPTOP_DOAN
go
USE LTW_BANLAPTOP_DOAN
GO
CREATE TABLE HANGMAY
( 
	MAHANG INT IDENTITY(1,1)PRIMARY KEY,
	TENHANG NVARCHAR(50)
)

CREATE TABLE KHACHHANG
(
	MAKH INT IDENTITY(1,1) PRIMARY KEY,
    HOTEN NVARCHAR(50),
    NGAYSINH DATE,
    GIOITINH NVARCHAR(3),
    DIENTHOAI INT,
    TAIKHOAN NVARCHAR(30),
    MATKHAU NVARCHAR(10),
    EMAIL NVARCHAR(30),
    DIACHI NVARCHAR(50),
	PHANQUYEN Varchar(30)
)
go
CREATE TRIGGER SetDefaultPhanQuyen
ON KhachHang
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE KhachHang
    SET PhanQuyen = 'User'
    FROM KhachHang
    inner join inserted ON KhachHang.MAKH = inserted.MAKH;
END;
go
CREATE TABLE DONHANG
(
	MADH INT IDENTITY(1,1) PRIMARY KEY,
	NGAYGIAO DATE,
	NGAYDAT DATE,
	DATHANHTOAN NVARCHAR(20),
	TINHTRANGGIAO NVARCHAR(50),
	MAKH INT,
	CONSTRAINT FK_KHACHHANG FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAKH)
);


CREATE TABLE NHASX
(	
	MANSX INT IDENTITY(1,1) PRIMARY KEY,
	TENNSX NVARCHAR(50),
	DIACHI NVARCHAR(50),
	DIENTHOAI INT
	
)

CREATE TABLE TINHTRANGMAY
(
	MATINHTRANG INT PRIMARY KEY,
	TENTINHTRANG NVARCHAR(50)
)
CREATE TABLE LAPTOP
(
	MALAP INT IDENTITY(1,1) PRIMARY KEY,
	TENLAP NVARCHAR(50),
	MATINHTRANG INT,
	GIABAN FLOAT,
	MOTA NVARCHAR(50),
	NGAYCAPNHAT DATE,
	ANHBIA NVARCHAR(200),
	SOLUONGTON INT,
	MAHANG INT,
	MANSX INT,
	MOI INT,
	
	CONSTRAINT FK_HM FOREIGN KEY (MAHANG) REFERENCES HANGMAY(MAHANG),
	CONSTRAINT FK_NSX FOREIGN KEY (MANSX) REFERENCES NHASX(MANSX),
	CONSTRAINT FK_TINHTRANGMAY FOREIGN KEY (MATINHTRANG) REFERENCES TINHTRANGMAY(MATINHTRANG),
)


CREATE TABLE CHITIETDONHANG
(
	MADH INT,
	MALAP INT,
	SOLUONG INT,
	DONGIA FLOAT,
	CONSTRAINT PK_CTDH PRIMARY KEY(MADH,MALAP),
	CONSTRAINT FK_CTDH_DH FOREIGN KEY (MADH) REFERENCES DONHANG(MADH),
	CONSTRAINT FK_CTDH_LAPTAP FOREIGN KEY(MALAP) REFERENCES LAPTOP(MALAP)

)
CREATE TABLE LOAITIN
(
	MLTIN INT IDENTITY(1,1) PRIMARY KEY,
	TLTIN NVARCHAR(50)
	
)
CREATE TABLE TINTUC
(
	MATIN INT IDENTITY(1,1) PRIMARY KEY,
	TIEUDETIN NVARCHAR(700),
	NOIDUNG NVARCHAR(700),
	NGAYDANG DATE,
	ANHTIN NVARCHAR(200),
	MLTIN INT,
	CONSTRAINT FK_TINTUC FOREIGN KEY (MLTIN) REFERENCES LOAITIN(MLTIN),
)
SET IDENTITY_INSERT HANGMAY ON;
INSERT INTO HANGMAY(MAHANG,TENHANG)
VALUES('1',N'HP'),
('2',N'ACER'),
('3',N'DELL'),
('4',N'ASUS'),
('5',N'LENOVO'),
('6',N'MACBOOK'),
('7',N'MASSTEL'),
('8',N'MSI'),
('9',N'SURFACE'),
('10',N'SINGPC')
SET IDENTITY_INSERT HANGMAY OFF;
SELECT * FROM HANGMAY

INSERT INTO TINHTRANGMAY(MATINHTRANG,TENTINHTRANG)
VALUES
(1,N'MÁY MỚI'),
(2,N'MÁY CŨ NHƯ MỚI 90%'),
(3,N'MÁY CŨ TRẦY XƯỚC')
SELECT * FROM TINHTRANGMAY

SET IDENTITY_INSERT KHACHHANG ON;
INSERT INTO KHACHHANG(MAKH, HOTEN, NGAYSINH, GIOITINH, DIENTHOAI, TAIKHOAN, MATKHAU, EMAIL, DIACHI)
VALUES('1',N'NGÔ THÀNH QUANG','2003-01-01', N'NAM',0923568352,'KH1','KH1','quang2003@gmail.com',N'PHÚ YÊN'),
('2',N'NGÔ THÀNH ĐẠT','1998-10-01', N'NAM',0923568910,'KH2','KH2','dat1998@gmail.com',N'TP HCM'),
('3',N'NGUYỄN NHẬT QUANG','2005-01-02', N'NAM',0923566987,'KH3','KH3','quang2005@gmail.com',N'LONG AN'),
('4',N'TRẦN VĂN A','2001-03-24', N'NAM',0923568912,'KH4','KH4','TVA2001@gmail.com',N'ĐỒNG NAI'),
('5',N'PHẠM MAI','1996-01-29', N'NỮ',0923563251,'KH5','KH5','mai@gmail.com',N'HƯNG YÊN'),
('6',N'NGUYỄN LÊ ANH KIỆT','2003-10-10', N'NAM',0923560231,'KH6','KH6','kiet@gmail.com',N'HẢI DƯƠNG'),
('7',N'LÊ PHƯƠNG','2003-06-10', N'NỮ',0923569657,'KH7','KH7','phuong23@gmail.com',N'TP HCM'),
('8',N'TRẦN THỊ BÉ','2002-09-17', N'NỮ',0923561254,'KH8','KH8','be2002@gmail.com',N'TP HCM'),
('9',N'NHẬT LỆ','1989-03-01', N'NỮ',0923560023,'KH9','KH9','le1989@gmail.com',N'TP HCM'),
('10',N'HÀ ANH KHOA','2006-06-05', N'NAM',0923563245,'KH10','KH10','khoa2006@gmail.com',N'CẦN THƠ')
SET IDENTITY_INSERT KHACHHANG OFF;
SELECT *FROM KHACHHANG

SET IDENTITY_INSERT DONHANG ON;
INSERT INTO DONHANG(MADH, NGAYGIAO, NGAYDAT, DATHANHTOAN, TINHTRANGGIAO, MAKH)
VALUES('1','2023-10-10', '2023-10-01', N'HOÀN TẤT', N'ĐÃ GIAO','1'),
('2','2022-09-10', '2022-09-01', N'HOÀN TẤT', N'ĐÃ GIAO','2'),
('3','2023-11-10', '2023-10-01', N'HOÀN TẤT', N'ĐÃ GIAO','3'),
('4','2023-10-10', '2023-10-01', N'HOÀN TẤT', N'ĐÃ GIAO','4'),
('5','2023-12-10', '2023-12-03', N'CHƯA HOÀN TẤT', N'CHƯA GIAO','5'),
('6','2022-03-10', '2023-03-01', N'HOÀN TẤT', N'ĐÃ GIAO','6'),
('7','2023-10-22', '2023-10-01', N'HOÀN TẤT', N'ĐÃ GIAO','7'),
('8','2023-01-15', '2023-01-04', N'HOÀN TẤT', N'ĐÃ GIAO','8'),
('9','2024-01-10', '2023-12-31', N'CHƯA HOÀN TẤT', N'CHƯA GIAO','9'),
('10','2024-02-10', '2024-02-01', N'CHƯA HOÀN TẤT', N'CHƯA GIAO','10')
SET IDENTITY_INSERT DONHANG OFF;
SELECT*FROM DONHANG

SET IDENTITY_INSERT NHASX ON;
INSERT INTO NHASX(MANSX, TENNSX, DIACHI, DIENTHOAI)
VALUES('1', N'TẬP ĐOÀN MACBOOK', N'Cn Mỹ Thuận, Huyện Mỹ Lộc, Tỉnh Nam Định', 0283824400),
('2', N'TẬP ĐOÀN HP', N'Công nghiệp Mỹ Thuận, Huyện Mỹ Lộc, Tỉnh Nam Định', 0283824400),
('3', N'TẬP ĐOÀN ACER', N'CT4A Bắc Linh Đàm – Hoàng Mai – Hà Nội', 1900190300),
('4', N'TẬP ĐOÀN ASUS', N'285 Cách Mạng Tháng 8, Phường 12, Quận 10, TP HCM', 0304965680),
('5', N'TẬP ĐOÀN DELL', N'Công nghiệp Mỹ Thuận, Huyện Mỹ Lộc, Tỉnh Nam Định',  0932745999),
('6', N'TẬP ĐOÀN LENOVO', N'Công nghiệp Mỹ Thuận, Huyện Mỹ Lộc, Tỉnh Nam Định', 0283824400),
('7', N'TẬP ĐOÀN MSI', N'Công nghiệp Mỹ Thuận, Huyện Mỹ Lộc, Tỉnh Nam Định', 0283824400),
('8', N'TẬP ĐOÀN SURFACE', N' Số 80 Hào Nam, Đống Đa, Hà Nội',  0932745999),
('9', N'TẬP ĐOÀN SINGPC', N'Số 5, Quốc Tử Giám, Đống Đa, Hà Nội', 0904864711),
('10', N'TẬP ĐOÀN MASSTEL', N'Số 9 Lê Văn Huân, Phường 13 ,Quận Tân Bình, TP HCM', 1900545470)
SET IDENTITY_INSERT NHASX OFF;
SELECT*FROM NHASX

SET IDENTITY_INSERT LAPTOP ON;
INSERT INTO LAPTOP(MALAP, TENLAP, MATINHTRANG, GIABAN, MOTA, NGAYCAPNHAT, ANHBIA, SOLUONGTON, MAHANG, MANSX, MOI)
VALUES('1',N'HP 15s fq5229TU i3 1215U (8U237PA)',1,11980000,N'Màn hình: 15.6", Full HD, i3, 1215U, 1.2GHz','2022-11-02',N'ANH1.PNG',100,'1','2',1),
('2',N'HP 240 G9 i3 1215U (6L1X7PA)',2,10960000,N'Màn hình: 14", Full HD, CPU: i3, 1215U, 1.2GHz','2022-12-02',N'ANH2.PNG',110,'1','2',1),
('3',N'HP 15s fq5162TU i5 1235U (7C134PA)',1,15980000,N'Màn hình: 15.6", Full HD, i5, 1215U, 1.2GHz','2022-11-02',N'ANH3.PNG',90,'1','2',1),
('4',N'Acer Aspire 5 Gaming A515 58GM 51LB i5 13420H',1,11950000,N'Màn hình: 14", Full HD, CPU: i3, 1215U, 1.2GHz','2022-12-02',N'ANH4.PNG',64,'2','3',1),
('5',N'Acer Aspire 3 A315 510P 32EF i3 N305',3,9960000,N'Màn hình: 14", Full HD, CPU: i3, 1215U, 1.2GHz','2022-07-02',N'ANH5.PNG',94,'2','3',1),
('6',N'Acer VivoBook X515MA N4020 (BR480W)',1,9960000,N'Màn hình: 14", Full HD, CPU: i3, 1215U, 1.2GHz','2022-05-17',N'ANH6.PNG',110,'2','3',1),
('7',N'Dell Inspiron 15 3530 i7 1355U (N3530I716W1)',1,19600000,N'Màn hình: 14", Full HD, CPU: i5, 1215U, 1.2GHz','2022-08-12',N'ANH7.JPG',35,'3','5',1),
('8',N'Dell Vostro 15 3520 i3 1215U (5M2TT1)',1,12260000,N'Màn hình: 14", Full HD, CPU: i3, 1215U, 1.2GHz','2022-04-02',N'ANH8.JPG',110,'3','5',1),
('9',N'Lenovo Ideapad 3 15ITL6 i3 1115G4 (82H803SGVN)',3,9860000,N'Màn hình: 14", Full HD, CPU: i3, 1215U, 1.2GHz','2022-12-03',N'ANH9.JPG',110,'5','6',1),
('10',N'ACER Vivobook 16 X1605VA i5 1335U (MB360W)',1,15990000,N'Màn hình: 16", Full HD, CPU: i5, 1335U, 1.3GHz','2022-11-02',N'ANH10.PNG',40,'2','3',1),
('11',N'Lenovo Ideapad 3 15ITL6 i5 1155G7 (82H803RRVN)',1,11530000,N'Màn hình: 14", Full HD, CPU: i3, 1215U, 1.2GHz','2022-03-02',N'ANH11.JPG',110,'5','6',1),
('12',N'MacBook Air 13 inch M1 2020 7-core GPU',2,12360000,N'Màn hình: 14", Full HD, CPU: i3, 1215U, 1.2GHz','2022-12-02',N'ANH12.JPG',50,'6','1',1),
('13',N'Asus Vivobook 15 X1504ZA i3 1215U (NJ102W)',1,13560000,N'Màn hình: 15.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-12-03',N'ANH13.JPG',110,'4','4',1),
('14',N'MacBook Pro 16 inch M2 Pro 2023 19-core GPU',1,29960000,N'Màn hình: 15.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-12-01',N'ANH14.JPG',120,'6','1',1),
('15',N'MacBook Pro 13 inch M2 2022 10-core GPU',1,33360000,N'Màn hình: 15.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-01-02',N'ANH15.JPG',115,'6','1',1),
('16',N'Masstel E116 N4020',1,3360000,N'Màn hình: 11", Full HD, CPU: i3, 1215U, 1.2GHz','2022-12-02',N'ANH16.JPG',110,'7','10',1),
('17',N'Masstel E140 N4120',2,4260000,N'Màn hình: 14.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-12-02',N'ANH17.JPG',89,'7','10',1),
('18',N'MSI Gaming GF63 Thin 11SC i5 11400H (664VN)',1,15360000,N'Màn hình: 11", Full HD, CPU: i3, 1215U, 1.2GHz','2022-07-02',N'ANH18.PNG',74,'8','7',1),
('19',N'Dell Inspiron 15 3520 i5 1235U (i5U085W11BLU)',1,16230000,N'Màn hình: 14", Full HD, CPU: i5, 1215U, 1.2GHz','2022-11-02',N'ANH19.PNG',119,'3','5',1),
('20',N'MSI Gaming Katana GF66 12UCK i7 12650H (804VN)',1,21360000,N'Màn hình: 1506", Full HD, CPU: i5, 1215U, 1.2GHz','2022-12-02',N'ANH20.PNG',110,'8','7',1),
('21',N'MSI Modern 15 B7M R5 7530U (231VN)',1,14360000,N'Màn hình: 11", Full HD, CPU: i3, 1215U, 1.2GHz','2022-09-01',N'ANH21.PNG',111,'8','7',1),
('22',N'Surface Pro 9 i7 1255U',1,41360000,N'Màn hình: 11", Full HD, CPU: i5, 1215U, 1.2GHz','2022-12-03',N'ANH22.JPG',110,'9','8',1),
('23',N'Surface Pro 9 i5 1235U',1,28560000,N'Màn hình: 15.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-12-03',N'ANH23.JPG',75,'9','8',1),
('24',N'SingPC M16 i7 1065G7 (M16i7108M5-W)',3,14560000,N'Màn hình: 15.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-12-03',N'ANH24.JPG',60,'10','9',1),
('25',N'Asus TUF Gaming F15 FX506HF i5 11400H',1,14660000,N'Màn hình: 15.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-10-03',N'ANH25.JPG',70,'4','4',1),
('26',N'Asus Vivobook X415EA i3 1115G4 (EK2034W)',1,9560000,N'Màn hình: 15.6", Full HD, CPU: i3, 1215U, 1.2GHz','2022-12-03',N'ANH26.JPG',110,'4','4',1),
('27',N'MacBook Pro 12 inch M2 2023 10-core GPU',1,30360000,N'Màn hình: 15.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-01-02',N'ANH27.JPG',105,'6','1',1),
('28',N'MacBook Pro 10 inch M2 2021 10-core GPU',1,28360000,N'Màn hình: 13.5", Full HD, CPU: i5, 1215U, 1.2GHz','2022-04-02',N'ANH28.JPG',70,'6','1',1),
('29',N'Asus Vivobook X425EB i5 1115G4 (EK2034W)',1,13560000,N'Màn hình: 15.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-11-03',N'ANH29.JPG',80,'4','4',1),
('30',N'Asus Vivobook X412EA i3 1115G4 (EK2034W)',2,10560000,N'Màn hình: 15.6", Full HD, CPU: i3, 1215U, 1.2GHz','2022-12-02',N'ANH30.JPG',90,'4','4',1),
('31',N'MSI Gaming BRAVO Thin 11SC i5 11400H (664VN)',1,18990000,N'Màn hình: 15.6", Full HD, CPU: i5, 1215U, 1.2GHz','2022-07-02',N'ANH31.PNG',74,'8','7',1)
SET IDENTITY_INSERT LAPTOP OFF;
SELECT*FROM LAPTOP


INSERT INTO CHITIETDONHANG(MADH, MALAP, SOLUONG, DONGIA)
VALUES('1','1', 2,11980000),
('1','2', 1,10960000),
('1','3', 2,15980000),
('2','6', 2,9960000),
('2','7', 2,19600000),
('3','8', 3,12260000),
('3','20', 2,21360000),
('3','16', 1,3360000),
('4','13', 2,13560000),
('5','24', 4,14560000)

SELECT*FROM CHITIETDONHANG

SET IDENTITY_INSERT LOAITIN ON;
INSERT INTO LOAITIN(MLTIN,TLTIN)
VALUES('1',N'Khuyến mãi'),
	  ('2',N'Tri ân khách hàng'),
	  ('3',N'Tin tức')
SET IDENTITY_INSERT LOAITIN OFF;
	  select * from LOAITIN

SET IDENTITY_INSERT TINTUC ON;
INSERT INTO TINTUC(MATIN,TIEUDETIN,NOIDUNG,NGAYDANG,ANHTIN,MLTIN)
VALUES('1',N'Trên tay Huawei MatePad Pro 11 2024: Thiết kế mỏng gọn, màn hình 2k, phần mềm được tối ưu tốt',N'Huawei đã chính thức là những sản phẩm mới của hãng bao gồm laptop và tablet. nó là sản phẩm khiến người like','2023-02-04','ANH1.PNG','3'),
	  ('2',N'Top các latop Acer Aspire đáng mua, giá tốt, đáp ứng đủ nhu cầu phổ thông',N'Acer Aspire là dòng laptop phổ thông của Acer hướng đến người tiêu dùng cơ bản, quan tâm nhiều đến mức hiệu suất, cấu hình so ','2022-03-12','ANH4.PNG','3'),
	  ('3',N'Cách bật một cửa sổ trên messager của Macbook một cách dễ dàng', N'là một trong thiết kế mỏng gọn được nhiều người ưu thích, màn hình 2k, phần mềm được tối ưu tốt','2023-02-04','ANH2.PNG','3'),
	  ('4',N'Acer Aspire 3 A314 35: Màn hình 14 inch, HD, tấm nền LCD và con chip itel Core N5100 cùng 4GB RAM và 256GB',N'Việc thực hiện video trên messager  Macbook là một trong những tính năng phổ biến mà nhiều người like.','2023-03-09','ANH23.JPG','3'),
	  ('5',N'Giảm thêm cho Laptop đến 20% khi mua kèm iphone tại điện máy xanh, Xem ngay!',N'Khuyến mãi siêu hấp hẫn dành cho fan . Deal hot lần này iphone giảm thêm cực sốc khi mua kèm iphone tại điện máy xanh. Cơ hội mua sắm siêu hời đã đến rồi!','2023-11-11','ANH11.JPG','1'),
	  ('6',N'Laptop Acer Aspire 3 A315 i5 1135G7/8GB/256GB/win11',N'Giảm thêm 2% khi mua cùng sản phẩm có giá trị cao hơn. Hoàn trả 200.000đ cho chủ thẻ tín dụng Home Credit khi thanh toán hóa đơn từ 5 triệu. Nhập mã VNPAY giảm từ 5% hóa đơn khi mua hàng','2023-10-06','ANH13.JPG','1'),
	  ('7',N'Laptop Acer Aspire 5 Graming A515 58GM 51LB i5 13420H/16BB/512GB/win11',N'Tặng balo Predator gaming. Giảm thêm 5% khi mua cùng sản phẩm có giá trị cao hơn,  Hoàn trả 200.000đ cho chủ thẻ tín dụng Home Credit khi thanh toán hóa đơn từ 5 triệu','2021-09-10','ANH3.PNG','1'),
	  ('8',N'Để giúp khách hàng có thêm cơ hội trải nghiệm sản phẩm chính hãng với giá ưu đãi cực khủng, LAPVIP đã triển khai chương trình “TRI ÂN KHÁCH HÀNG - MUÔN NGÀN ƯU ĐÃI” trên tất cả các dòng laptop với nhiều quà tặng hấp dẫn',N'Nhân dịp cuối năm, LAPVIP xin gửi lời cảm ơn tới những quý  khách hàng đã đồng hành cùng chúng tôi trong suốt nhiều năm qua.','2023/11/22','ANH7.JPG','2'),
	  ('9',N'Laptop VUI Trân Trọng Thông Báo',N'Nhằm tri ân khách hàng trong suốt thời gian qua đã luôn tin tưởng và ủng hộ, cũng như tất cả các quý khách hàng đã, đang và sắp sử dụng sản phẩm của LAPTOP VUI.Tặng 200k cho tất cả các quý khách hàng mua laptop lần thứ 2, thứ 3, thứ n đối với tất cả laptop có giá trị từ 5.0 triệu trở lên.','2023-11-13','ANH15.JPG','2')

SET IDENTITY_INSERT TINTUC OFF;
SELECT*FROM TINTUC



UPDATE KHACHHANG
set PhanQuyen = 'Admin'
where makh = 1
select* from khachhang