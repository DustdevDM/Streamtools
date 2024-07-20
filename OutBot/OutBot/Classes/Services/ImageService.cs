using Discord.WebSocket;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace OutBot.Classes.Services
{
    /// <summary>
    /// Helper class to create images containing dynamic content
    /// </summary>
    internal abstract class ImageService
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        /// <param name="httpClient">Instance of <see cref="HttpClient"/></param>
        internal ImageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Generates image that greats a specific user of a server.
        /// </summary>
        /// <param name="guildUser">User that joined a guild</param>
        /// <returns>Returns an async <see cref="Stream"/> containing the image</returns>
        internal async Task<Stream> GenerateWelcomeImage(SocketGuildUser guildUser)
        {
            using Image image = await Image.LoadAsync("Resources/WelcomeBannerBase.png");

            int height = image.Height;
            const int profilePictureSize = 128;

            string profilePictureUrl = guildUser.GetAvatarUrl(size: 128) ?? guildUser.GetDefaultAvatarUrl();
            Stream profilePictureStream = await this.httpClient.GetStreamAsync(profilePictureUrl);
            Image<Rgba32> profilePicture = await Image.LoadAsync<Rgba32>(profilePictureStream);
            profilePicture.Mutate(ctx => ctx.Resize(profilePictureSize, profilePictureSize));

            Point profilePictureLocation = new (131, (height / 2) - (profilePictureSize / 2));
            profilePicture = profilePicture.Clone(imageProcessingContext => ConvertToAvatar(
                imageProcessingContext,
                new Size(profilePictureSize, profilePictureSize),
                64));
            image.Mutate(imageProcessingContext => imageProcessingContext.DrawImage(profilePicture, profilePictureLocation, 1f));

            Font welcomeFont = SystemFonts.CreateFont("Rubik", 20);
            string welcomeText = $"WILLKOMMEN AUF {guildUser.Guild.Name}!";
            Color welcomeTextColor = new (new Rgba32(0xe8, 0xe8, 0xe8));
            RectangleF welcomeTextArea = new (260, 75, 410, 30);
            this.CreateCenteredTextBox(image, welcomeText, welcomeFont, welcomeTextColor, welcomeTextArea);

            Font userNameFont = SystemFonts.CreateFont("Rubik", 44);
            string userNameText = $"@{guildUser.Username.ToUpper()}";
            Color userNameTextColor = new (new Rgba32(0x00, 0xF4, 0x8E));
            RectangleF userNameTextArea = new (260, 80, 410, 71);
            this.CreateCenteredTextBox(image, userNameText, userNameFont, userNameTextColor, userNameTextArea);

            MemoryStream stream = new ();
            await image.SaveAsPngAsync(stream);
            stream.Position = 0;

            return stream;
        }

        private void CreateCenteredTextBox(Image image, string text, Font font, Color fontColor, RectangleF textArea)
        {
            FontRectangle textSize;
            do
            {
                textSize = TextMeasurer.MeasureSize(text, new TextOptions(font));
                font = SystemFonts.CreateFont(font.Family.Name, font.Size - 1, FontStyle.Bold);
                if (Math.Abs(font.Size - 1.0) < 1)
                    break;
            }
            while (textSize.Width > textArea.Width && font.Size > 1);

            PointF textLocation = new (
                textArea.X + ((textArea.Width - textSize.Width) / 2),
                textArea.Y + ((textArea.Height - textSize.Height) / 2));
            image.Mutate(imageProcessingContext => imageProcessingContext.DrawText(text, font, fontColor, textLocation));
        }

        /// <src>https://github.com/SixLabors/Samples/blob/24326f576f712b190b8f05ab230a5c9b95d90fcf/ImageSharp/AvatarWithRoundedCorner/Program.cs</src>
        private static void ConvertToAvatar(IImageProcessingContext context, Size size, float cornerRadius)
        {
            ApplyRoundedCorners(
                context.Resize(new ResizeOptions
            {
                Size = size,
                Mode = ResizeMode.Crop,
            }),
                cornerRadius);
        }

        /// <src>https://github.com/SixLabors/Samples/blob/24326f576f712b190b8f05ab230a5c9b95d90fcf/ImageSharp/AvatarWithRoundedCorner/Program.cs</src>
        private static void ApplyRoundedCorners(IImageProcessingContext context, float cornerRadius)
        {
            Size size = context.GetCurrentSize();
            IPathCollection corners = BuildCorners(size.Width, size.Height, cornerRadius);

            context.SetGraphicsOptions(new GraphicsOptions()
            {
                Antialias = true,
                AlphaCompositionMode = PixelAlphaCompositionMode.DestOut,
            });

            corners.Aggregate(context, (current, path) => current.Fill(Color.Red, path));
        }

        /// <src>https://github.com/SixLabors/Samples/blob/24326f576f712b190b8f05ab230a5c9b95d90fcf/ImageSharp/AvatarWithRoundedCorner/Program.cs</src>
        private static PathCollection BuildCorners(int imageWidth, int imageHeight, float cornerRadius)
        {
            RectangularPolygon rect = new (-0.5f, -0.5f, cornerRadius, cornerRadius);

            IPath cornerTopLeft = rect.Clip(new EllipsePolygon(cornerRadius - 0.5f, cornerRadius - 0.5f, cornerRadius));

            float rightPos = imageWidth - cornerTopLeft.Bounds.Width + 1;
            float bottomPos = imageHeight - cornerTopLeft.Bounds.Height + 1;

            IPath cornerTopRight = cornerTopLeft.RotateDegree(90).Translate(rightPos, 0);
            IPath cornerBottomLeft = cornerTopLeft.RotateDegree(-90).Translate(0, bottomPos);
            IPath cornerBottomRight = cornerTopLeft.RotateDegree(180).Translate(rightPos, bottomPos);

            return new PathCollection(cornerTopLeft, cornerBottomLeft, cornerTopRight, cornerBottomRight);
        }
    }
}
