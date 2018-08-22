using UnityEngine;
using System.Collections;
using System;

namespace Renlvda.Util{
	[Serializable]
	public struct Vector3int
	{
		//默认常量
		public static readonly Vector3int zero = new Vector3int (0, 0, 0);
		public static readonly Vector3int one = new Vector3int (1, 1, 1);
		public static readonly Vector3int forward = new Vector3int (0, 0, 1);
		public static readonly Vector3int back = new Vector3int (0, 0, -1);
		public static readonly Vector3int up = new Vector3int (0, 1, 0);
		public static readonly Vector3int down = new Vector3int (0, -1, 0);
		public static readonly Vector3int left = new Vector3int (-1, 0, 0);
		public static readonly Vector3int right = new Vector3int (1, 0, 0);

		public int x,y,z;

		//构造函数
		public Vector3int(int x = 0,int y = 0,int z = 0){
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public Vector3int(Vector3 pos){
			this.x = Mathf.FloorToInt (pos.x);
			this.y = Mathf.FloorToInt (pos.y);
			this.z = Mathf.FloorToInt (pos.z);
		}

		//计算函数
		public static Vector3int Mul(Vector3int a,Vector3int b){
			return new Vector3int (a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static Vector3int Div(Vector3int a,Vector3int b){
			return new Vector3int (a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public static Vector3int Min(Vector3int a,Vector3int b){
			return Process (a, b, Mathf.Min);
		}

		public static Vector3int Max(Vector3int a,Vector3int b){
			return Process (a, b, Mathf.Max);
		}

		public static Vector3int Abs(Vector3int a){
			return Process (a, Mathf.Abs);
		}

		public static Vector3int Floor(Vector3 a){
			return Vector3Utils.ProcessTo3int (a, Mathf.FloorToInt);
		}

		public static Vector3int Ceil(Vector3 a){
			return Vector3Utils.ProcessTo3int (a, Mathf.CeilToInt);
		}

		public static Vector3int Round(Vector3 a){
			return Vector3Utils.ProcessTo3int (a, Mathf.RoundToInt);
		}

		public static Vector3int Process(Vector3int v,Func<int,int> func){
			v.x = func (v.x);
			v.y = func (v.y);
			v.z = func (v.z);
			return v;
		}

		public static Vector3int Process(Vector3int a, Vector3int b, Func<int,int,int> func){
			a.x = func (a.x, b.x);
			a.y = func (a.y, b.y);
			a.z = func (a.z, b.z);
			return a;
		}


		//操作符重载
		public static Vector3int operator - (Vector3int a){
			return new Vector3int (-a.x, -a.y, -a.z);
		}
		public static Vector3int operator - (Vector3int a,Vector3int b){
			return new Vector3int (a.x - b.x, a.y - b.y, a.z - b.z);
		}
		public static Vector3int operator + (Vector3int a,Vector3int b){
			return new Vector3int (a.x + b.x, a.y + b.y, a.z + b.z);
		}
		public static Vector3int operator * (Vector3int a,int factor){
			return new Vector3int (a.x * factor, a.y * factor, a.z * factor);
		}
		public static Vector3int operator / (Vector3int a,int factor){
			return new Vector3int (a.x / factor, a.y / factor, a.z / factor);
		}
		public static Vector3int operator * (Vector3int a,Vector3int b){
			return Mul (a, b);
		}
		public static Vector3int operator / (Vector3int a,Vector3int b){
			return Div (a, b);
		}

		public static bool operator == (Vector3int a,Vector3int b){
			return a.x == b.x && 
				   a.y == b.y && 
				   a.z == b.z;
		}

		public static bool operator != (Vector3int a,Vector3int b){
			return a.x != b.x || 
				   a.y != b.y || 
				   a.z != b.z;
		}

		public static implicit operator Vector3(Vector3int v){
			return new Vector3 (v.x, v.y, v.z);
		}

		//重写方法
		public override bool Equals(object other){
			if (other is Vector3int == false)
				return false;
			Vector3int vector = (Vector3int)other;
			return x == vector.x && 
				   y == vector.y && 
				   z == vector.z;
		}

		public override int GetHashCode(){
			return x.GetHashCode () ^ y.GetHashCode () << 2 ^ z.GetHashCode () >> 2;
		}

		public override string ToString ()
		{
			return string.Format ("Vector3int({0},{1},{2})", x, y, z);
		}

		//Vector3的拓展工具类
		public static class Vector3Utils
		{
			public static Vector3 Mul(this Vector3 a,Vector3 b){
				a.x *= b.x;
				a.y *= b.y;
				a.z *= b.z;
				return a;
			}

			public static Vector3 Div(this Vector3 a,Vector3 b){
				a.x /= b.x;
				a.y /= b.y;
				a.z /= b.z;
				return a;
			}

			public static Vector3 Process(this Vector3 a,Func<float,float> func){
				a.x = func (a.x);
				a.y = func (a.y);
				a.z = func (a.z);
				return a;
			}

			public static Vector3int ProcessTo3int(this Vector3 a,Func<float,int> func){
				Vector3int vi;
				vi.x = func (a.x);
				vi.y = func (a.y);
				vi.z = func (a.z);
				return vi;
			}
		}
	}
}