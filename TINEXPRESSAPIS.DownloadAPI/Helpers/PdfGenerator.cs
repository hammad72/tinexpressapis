
using Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace TINEXPRESSAPIS.DownloadAPI.Helpers
{
    public static class PdfGenerator
    {
        public static byte[] GenerateCourierLabelPdf(orderdetails label)
        {
            try
            {
                var barcode = BarcodeHelper.GenerateBarcode(label.order_number);
                var qr = BarcodeHelper.GenerateQrCode(label.order_number);
                string txLogo = Path.Combine(Directory.GetCurrentDirectory(), "public", "logo.png");
                string courier_logo = "";
                if (label.courier_id == 1)
                {
                    courier_logo = Path.Combine(Directory.GetCurrentDirectory(), "public", "australia_post.png");
                    //courier_logo = "public/australia_post.png";
                }
                else if (label.courier_id == 2)
                {
                    courier_logo = Path.Combine(Directory.GetCurrentDirectory(), "public", "couriers-please.png");
                    //courier_logo = "public/couriers-please.png";
                }
                else if (label.courier_id == 3)
                {
                    courier_logo = Path.Combine(Directory.GetCurrentDirectory(), "public", "hunter_express1.png");
                    //courier_logo = "public/hunter_express1.png";
                }
                else if (label.courier_id == 4)
                {
                    courier_logo = Path.Combine(Directory.GetCurrentDirectory(), "public", "tnt.png");
                    //courier_logo = "public/tnt.png";
                }
                else if (label.courier_id == 5)
                {
                    courier_logo = Path.Combine(Directory.GetCurrentDirectory(), "public", "zoom2u.png");
                    //courier_logo = "public/zoom2u.png";
                }
                else if (label.courier_id == 6)
                {
                    courier_logo = Path.Combine(Directory.GetCurrentDirectory(), "public", "Aramex.png");
                    //courier_logo = "public/Aramex.png";
                }
                else
                {
                    courier_logo = Path.Combine(Directory.GetCurrentDirectory(), "public", "logo.png");
                    //courier_logo = "public/logo.png";
                }

                if (!File.Exists(txLogo))
                {
                    throw new FileNotFoundException("TX logo not found", txLogo);
                }
                if (!File.Exists(courier_logo))
                {
                    throw new FileNotFoundException("Courier logo not found", courier_logo);
                }
                using var stream = new MemoryStream();

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A6);
                        page.Margin(10);
                        page.DefaultTextStyle(x => x.FontSize(9));
                        page.Content().Column(col =>
                        {
                            // Header
                            //col.Item().Background("#7f56da").Height(30).Image("public/logo1.png");

                            col.Item().Row(row =>
                            {
                                row.RelativeItem().Column(colLeft =>
                                {
                                    colLeft.Item().AlignLeft().MaxHeight(30).Image(txLogo);
                                });

                                row.RelativeItem().Column(colRight =>
                                {
                                    colRight.Item().AlignRight().MaxHeight(30).Image(courier_logo);
                                });
                            });
                            col.Item().PaddingTop(5).Background("#7f56da").Height(3);

                            // "To" section
                            col.Item().PaddingTop(5).Border(1).BorderColor(Colors.Grey.Medium).Column(to =>
                            {
                                to.Item().PaddingTop(5).PaddingLeft(5).Text("To:").Bold();
                                to.Item().PaddingLeft(5).Text($"{label.reciever_name}");
                                to.Item().PaddingLeft(5).Text($"{label.address_dropoff}");
                                to.Item().PaddingLeft(5).Text($"{label.suburb_dropoff}, {label.state_dropoff} {label.postcode_dropoff}");
                                to.Item().PaddingLeft(5).Text("Leave parcel at reception");
                                to.Item().PaddingLeft(5).PaddingBottom(5).Text($"Ph: {label.reciever_phone}");
                            });

                            // Delivery info
                            //col.Item().PaddingTop(5).Row(row =>
                            //{
                            //    row.RelativeItem().Text($"Weight: {label.consignment_number} kg\nTracking#:\n{label.consignment_number}").Bold();
                            //    row.RelativeItem().Text("Delivery features\n✔ Signature on delivery\n✔ Tracking");
                            //});
                            col.Item().PaddingTop(5).Row(row =>
                            {
                                row.RelativeItem().Text($"TINX Ref#:\n{label.consignment_number}").Bold();
                                row.RelativeItem().Text($"Courier Ref#:\n{label.order_number}").Bold();
                                //row.RelativeItem().Text("Delivery features\n✔ Signature on delivery\n✔ Tracking");
                            });

                            // From section
                            col.Item().PaddingTop(5).Border(1).BorderColor(Colors.Grey.Medium).Column(from =>
                            {
                                from.Item().PaddingTop(5).PaddingLeft(5).Text("From:").Bold();
                                from.Item().PaddingLeft(5).Text($"{label.sender_name}");
                                from.Item().PaddingLeft(5).Text($"{label.address_pickup}");
                                from.Item().PaddingLeft(5).Text($"{label.suburb_pickup}, {label.state_pickup} {label.postcode_pickup}");
                                from.Item().PaddingLeft(5).PaddingBottom(5).Text($"Ph: {label.sender_phone}");
                            });

                            // QR and Barcode
                            //col.Item().PaddingTop(5).Row(row =>
                            //{
                            //    row.RelativeItem().Image(barcode).Height(40);
                            //    row.ConstantItem(80).Image(qr).FitArea();
                            //});

                            //col.Item().PaddingTop(5).Row(row =>
                            //{
                            //    row.RelativeItem().Height(50).Width(50).Image(qr);
                            //    row.RelativeItem().PaddingTop(5).Image(barcode);
                            //});
                            //col.Item().PaddingTop(5).Row(row =>
                            //{
                            //    row.RelativeItem().Text($"{label.order_number}").Bold();
                            //    row.RelativeItem().Text($"{label.order_number}").Bold();
                            //});
                            col.Item().PaddingTop(5).Row(row =>
                            {
                                row.RelativeItem().Column(colLeft =>
                                {
                                    colLeft.Item().AlignLeft().MaxHeight(50).Image(qr);
                                    colLeft.Item().AlignLeft().Text(label.order_number).FontSize(8);
                                });

                                row.RelativeItem().Column(colRight =>
                                {
                                    colRight.Item().AlignCenter().Image(barcode);
                                    colRight.Item().AlignCenter().Text(label.order_number).FontSize(8);
                                });
                            });

                            // Declaration
                            //col.Item().PaddingTop(5).Text(txt =>
                            //{
                            //    txt.Span("Aviation Security and Dangerous Goods Declaration").Bold().FontSize(6).FontColor(Colors.Grey.Medium);
                            //    txt.Span(": This article may be carried by air and must be safe for air transport. Dangerous goods are not permitted.")
                            //       .FontSize(6).FontColor(Colors.Grey.Medium);
                            //});
                        });
                    });
                }).GeneratePdf(stream);

                return stream.ToArray();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
