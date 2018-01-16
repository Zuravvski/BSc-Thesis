#pragma once
#include <json.hpp>
#include <ostream>

using nlohmann::json;

struct Position
{
	Position() : x(-1), y(-1), angle(-1), identified(false) {}
	Position(float x, float y, float angle, bool identified = false) : x(x), y(y), angle(angle), identified(identified) {}

	float x;
	float y;
	float angle;
	bool identified;

	void to_json(json& j, const Position& position) 
	{
		j = json { { "x", position.x }, { "y", position.y }, { "angle", position.angle }, { "identified", position.identified} };
	}

	void from_json(const json& json, Position& position)
	{
		position.x = json.at("x").get<float>();
		position.y = json.at("y").get<float>();
		position.angle = json.at("angle").get<float>();
		position.identified = json.at("identified").get<bool>();
	}

	friend std::ostream& operator<<(std::ostream& os, const Position& obj)
	{
		return os
			<< "x: " << obj.x
			<< " y: " << obj.y
			<< " angle: " << obj.angle
			<< " identified: " << std::boolalpha << obj.identified;
	}
};