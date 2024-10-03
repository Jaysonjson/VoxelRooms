shader_type canvas_item;

uniform sampler2D st : hint_screen_texture, filter_linear_mipmap;
uniform float amount: hint_range(0.0, 7.0);
uniform float time_mod: hint_range(0, 10.0);
uniform vec3 color_mod;
uniform float y_mod: hint_range(0, 25.0);
uniform float x_mod: hint_range(0, 25.0);

void vertex() {
	VERTEX.y += sin(TIME * time_mod + VERTEX.x * 7.0) * y_mod;
	VERTEX.x += sin(TIME * time_mod) * x_mod;
}

void fragment() {
	//COLOR.rgb = textureLod(st, SCREEN_UV, amount).rgb * sin(TIME) + 1.0;
	COLOR.rgb = textureLod(st, SCREEN_UV, amount).rgb * color_mod;
	COLOR.a = amount;
}
