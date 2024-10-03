shader_type canvas_item;

uniform vec4 fog_color : source_color; 
uniform sampler2D screen_tex : hint_screen_texture, filter_linear_mipmap;

vec4 noiseScroll(float offsetX, float offsetY, float x, float y, sampler2D rect) {
	return texture(rect, vec2(x / offsetX + TIME / 100.0, y / offsetY - TIME / 125.0));
}

vec4 noiseSinScroll(float offsetX, float offsetY, float x, float y, sampler2D rect) {
	return texture(rect, vec2(x / offsetX + cos(TIME) * TIME / 100000.0, y / offsetY - (sin(TIME) * 2.5) * TIME / 100000.0));
}

vec4 noiseCosScroll(float offsetX, float offsetY, float x, float y, sampler2D rect) {
	return texture(rect, vec2(x / offsetX + (sin(TIME) * 2.5) * TIME / 100000.0, y / offsetY - cos(TIME) * TIME / 100000.0));
}

vec4 noiseTScroll(float offsetX, float offsetY, float x, float y, sampler2D rect) {
	return texture(rect, vec2(x / offsetX + cos(TIME) * TIME / 100000.0, y / offsetY - cos(TIME) * TIME / 100000.0));
}

void fragment() {	
	COLOR = mix(noiseScroll(2.0, 2.0, UV.x, UV.y, TEXTURE), noiseScroll(1.0, 2.0, UV.x, UV.y, TEXTURE), noiseScroll(3.2, 2.0, UV.x, UV.y, TEXTURE));
	COLOR.rgb = 1.0 - (1.0 - COLOR.rgb) / fog_color.rgb * texture(screen_tex, SCREEN_UV).rgb;
	COLOR.rgb = COLOR.rgb * fog_color.rgb;
	COLOR.a = fog_color.a;
}
