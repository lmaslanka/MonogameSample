namespace FirstGame
{
    using Microsoft.Xna.Framework.Graphics;

    public class Tile
    {
        public Tile()
        {
            this.vertex1 = new VertexPositionTexture();
            this.vertex2 = new VertexPositionTexture();
            this.vertex3 = new VertexPositionTexture();
            this.vertex4 = new VertexPositionTexture();
            this.vertex5 = new VertexPositionTexture();
            this.vertex6 = new VertexPositionTexture();

            this.vertex1Count = 0;
            this.vertex2Count = 0;
            this.vertex3Count = 0;
            this.vertex4Count = 0;
            this.vertex5Count = 0;
            this.vertex6Count = 0;
    }

        public VertexPositionTexture vertex1;
        public VertexPositionTexture vertex2;
        public VertexPositionTexture vertex3;
        public VertexPositionTexture vertex4;
        public VertexPositionTexture vertex5;
        public VertexPositionTexture vertex6;

        public int vertex1Count;
        public int vertex2Count;
        public int vertex3Count;
        public int vertex4Count;
        public int vertex5Count;
        public int vertex6Count;
    }
}
