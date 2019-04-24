Shader "Mobile Shaders/Simple Color TwoSided"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1)
	}

	SubShader
	{
	cull Off

		Color[_Color]
		Pass{}
	}
}