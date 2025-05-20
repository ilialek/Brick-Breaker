﻿using System;
using GXPEngine;	// For Mathf

public struct Vec2 
{
	public float x;
	public float y;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}

	public override string ToString () 
	{
		return String.Format ("({0},{1})", x, y);
	}

	public void SetXY(float pX, float pY) 
	{
		x = pX;
		y = pY;
	}

	public float Length() {
		return Mathf.Sqrt (x * x + y * y);
	}

	public void Normalize() {
		float len = Length ();
		if (len > 0) {
			x /= len;
			y /= len;
		}
	}

	public Vec2 Normalized() {
		Vec2 result = new Vec2 (x, y);
		result.Normalize ();
		return result;
	}

    public float Dot(Vec2 other)
    {
        float dotProduct = x * other.x + y * other.y;
        return dotProduct;
    }

    public Vec2 Normal()
    {
        return new Vec2(-y / this.Length(), x / this.Length());
    }

	public float CrossProduct(Vec2 other)
	{
		return x * other.y - y * other.x;
	}

	public Vec2 RotatedVec2(float angle) {
		Vec2 vx = new Vec2(x * Mathf.Cos(angle), x * Mathf.Sin(angle));
        Vec2 vy = new Vec2(-y * Mathf.Sin(angle), y * Mathf.Cos(angle));
		return vx + vy;
    }

    public static Vec2 operator +(Vec2 left, Vec2 right) {
		return new Vec2 (left.x + right.x, left.y + right.y);
	}

	public static Vec2 operator -(Vec2 left, Vec2 right) {
		return new Vec2 (left.x - right.x, left.y - right.y);
	}

	public static Vec2 operator *(Vec2 v, float scalar) {
		return new Vec2 (v.x * scalar, v.y * scalar);
	}

	public static Vec2 operator *(float scalar, Vec2 v) {
		return new Vec2 (v.x * scalar, v.y * scalar);
	}

	public static Vec2 operator /(Vec2 v, float scalar) {
		return new Vec2 (v.x / scalar, v.y / scalar);
	}
}

