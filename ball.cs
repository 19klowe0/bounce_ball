using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace bounce_Ball
{
    class ball
    {
        private Vector3 pos;
        private Vector3 vel;
        private float radius;

        public Vector3 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public Vector3 Vel
        {
            get { return vel; }
            set { vel = value; }
        }

        public BoundingSphere boundingSphere
        {
            get { return new BoundingSphere(pos, radius); }
        }

        public ball(Vector3 p, float r)
        {
            pos = p;
            radius = r;
            vel = Vector3.Zero;
        }

        public void Update(GameTime gameTime)
        {
            Vector3 gravity = new Vector3(0, -3, 0);
            vel += gravity;
            pos += vel * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void checkCollision(Plane other)
        {

            if (PlaneIntersectionType.Intersecting == new BoundingSphere(this.pos, this.radius).Intersects(other))
            {

                // get the distance between the center of the ball & plane 
                float dist = Vector3.Dot(other.Normal, this.pos - (other.Normal * other.D));
                //D: the distance of the plane along its normal vector from the orgin 
                // the dot product of the plane's normal vector and the point vector micus thte posint-in-plane 

                if (dist < this.radius)
                {
                    pos = pos + (other.Normal * (radius - dist));
                }

                if (this.vel.Length() > 0.001f)
                {
                    //get the direction vector
                    Vector3 V = this.vel;
                    V.Normalize();

                    //get the new direction 
                    V = 2 * Vector3.Dot(-V, other.Normal) * other.Normal + V;

                    //set the new direction
                    this.vel = this.vel.Length() * V;

                }
            }
        }
        public void checkCollsion(ball other)
        {

            if(new BoundingSphere(this.pos, this.radius).Intersects(new BoundingSphere(other.pos, other.radius)))
            {
                Vector3 axis = other.pos - this.pos;
                float dist = other.radius + this.radius;
                float move = (dist - axis.Length()) / 2f;
                axis.Normalize();

                Vector3 U1x = axis * (Vector3.Dot(axis, this.vel));
                Vector3 U1y = this.vel - U1x;

                Vector3 U2x = -axis * Vector3.Dot(-axis, other.vel);
                Vector3 U2y = other.vel - U2x;

                Vector3 V1x = U2x;

                Vector3 V2x = U1x;

                this.vel = V1x + U1y;
                other.vel = V2x + U2y;

                this.vel *= 0.99f;
                other.vel *= .99f;

                other.pos += axis * move;
                this.pos -= axis * move;


            }

        }
    }
}
