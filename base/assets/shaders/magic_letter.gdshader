shader_type canvas_item;

uniform float time_mod: hint_range(0, 59555.0);
uniform float y_mod: hint_range(0, 55.0);
uniform float x_mod: hint_range(0, 55.0);

void vertex() {
	VERTEX.y += sin(TIME * time_mod + VERTEX.x * 7.0) * y_mod * PI;
	VERTEX.x += sin(TIME * time_mod) * x_mod * PI;
}
