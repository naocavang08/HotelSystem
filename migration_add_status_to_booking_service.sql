-- Script SQL để thêm cột status vào bảng BookingServices
-- Kiểm tra xem cột đã tồn tại chưa, nếu chưa thì thêm vào
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[BookingServices]') AND name = 'status')
BEGIN
    ALTER TABLE [dbo].[BookingServices]
    ADD [status] VARCHAR(50) NOT NULL DEFAULT 'Booked'
END
GO

-- Cập nhật giá trị mặc định cho các bản ghi hiện có
UPDATE [dbo].[BookingServices]
SET [status] = 'Booked'
WHERE [status] IS NULL
GO 