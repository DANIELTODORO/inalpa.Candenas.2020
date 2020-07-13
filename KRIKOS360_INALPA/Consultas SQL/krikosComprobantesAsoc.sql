USE [SBDAMODE]
GO

/****** Object:  StoredProcedure [dbo].[KrikosComprobantesAsoc]    Script Date: 13/07/2020 08:32:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[KrikosComprobantesAsoc]  @ComprobanteSDK_id int
AS
select spvtco_TipoFijo,spv_CodPvt,spv_Nro 
from SegTiposV 
where spvscv_ID in (SELECT  dbo.SegRelDetV.srvscv_IDOrig
FROM            dbo.SegRelDetV 
WHERE        dbo.SegRelDetV.srvscv_ID IN
                             (SELECT        cvescv_ID
                               FROM            dbo.CabVenta where cve_ID = @ComprobanteSDK_id ))
UNION
select spvtco_TipoFijo,spv_CodPvt,spv_Nro
from SegTiposV 
where spvscv_ID in (SELECT  dbo.SegRelDetV.srvscv_ID
FROM            dbo.SegRelDetV 
WHERE        dbo.SegRelDetV.srvscv_IDOrig IN
                             (SELECT        cvescv_ID
                               FROM            dbo.CabVenta where cve_ID =@ComprobanteSDK_id ))
GO

