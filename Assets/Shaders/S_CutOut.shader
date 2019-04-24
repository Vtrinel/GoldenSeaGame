
Shader "Mobile Shaders/CutOut" 
{
	Properties 
	{
		_MainTex ("Texture", 2D) = "white" {}
		//_Color(" Color", color) = (1,1,1,1)
	}

	Category 
	{
		Tags { "Queue"="Transparent"  }
		Blend SrcAlpha OneMinusSrcAlpha
    
		SubShader 
		{
			Pass 
			{
				SetTexture [_MainTex]
				//SetColor [_Color]
			}
		}
	}
}