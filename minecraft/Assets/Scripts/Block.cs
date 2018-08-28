using UnityEngine;
using System.Collections;

//方块的方向
public enum BlockDirection : byte
{
	Front = 0,
	Back = 1,
	Left = 2,
	Right = 3,
	Top = 4,
	Bottom = 5,
}

//方块对象，存储方块的所有信息
public class Block{

	//方块的ID
	public byte id;
	//方块的名字
	public string name;
	//方块的图标，并不会采用在游戏中动态生成的做法
	public Texture icon;
	//方向（指的是前面所面朝的方向）
	public BlockDirection direction = BlockDirection.Front;

	//前面贴图的坐标
	public byte textureFrontX;
	public byte textureFrontY;

	//后面贴图的坐标
	public byte textureBackX;
	public byte textureBackY;

	//右面贴图的坐标
	public byte textureRightX;
	public byte textureRightY;

	//左面贴图的坐标
	public byte textureLeftX;
	public byte textureLeftY;

	//上面贴图的坐标
	public byte textureTopX;
	public byte textureTopY;

	//下面贴图的坐标
	public byte textureBottomX;
	public byte textureBottomY;

	//都是A面的方块
	public Block (byte id, string name, byte textureX, byte textureY)
		: this (id, name, textureX, textureY, textureX, textureY, textureX, textureY, textureX, textureY)
	{
		
	}

	//上面是A，其它面是B的方块
	public Block (byte id, string name, byte textureX, byte textureY, byte textureTopX, byte textureTopY)
		: this (id, name, textureX, textureY, textureX, textureY, textureX, textureY, 
			textureX, textureY, textureTopX, textureTopY, textureX, textureY)
	{
		
	}

	//上面是A，下面是B，其它面是C的方块
	public Block (byte id, string name, byte textureX, byte textureY, byte textureTopX, byte textureTopY, 
		byte textureBottomX, byte textureBottomY) 
		: this (id, name, textureX, textureY, textureX, textureY, textureX, textureY, textureX, textureY,
			textureTopX, textureTopY, textureBottomX, textureBottomY)
	{
		
	}


	//上面是A，下面是B，前面是C，其它面是D的方块
	public Block (byte id, string name, byte textureFrontX, byte textureFrontY, byte textureX, byte textureY, 
		byte textureTopX, byte textureTopY, byte textureBottomX, byte textureBottomY) 
		: this (id, name, textureFrontX, textureFrontY, textureX, textureY, textureX, textureY, textureX, textureY,
			textureTopX, textureTopY, textureBottomX, textureBottomY)
	{
		
	}

	//上下左右前后面都不一样的方块
	public Block (byte id, string name, byte textureFrontX, byte textureFrontY, byte textureBackX, byte textureBackY, 
		byte textureRightX, byte textureRightY, byte textureLeftX, byte textureLeftY, byte textureTopX, byte textureTopY,
		byte textureBottomX, byte textureBottomY)
	{
		this.id = id;
		this.name = name;

		this.textureFrontX = textureFrontX;
		this.textureFrontY = textureFrontY;

		this.textureBackX = textureBackX;
		this.textureBackY = textureBackY;

		this.textureRightX = textureRightX;
		this.textureRightY = textureRightY;

		this.textureLeftX = textureLeftX;
		this.textureLeftY = textureLeftY;

		this.textureTopX = textureTopX;
		this.textureTopY = textureTopY;

		this.textureBottomX = textureBottomX;
		this.textureBottomY = textureBottomY;
	}


}
