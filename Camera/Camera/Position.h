#pragma once
#include <json.hpp>
#include <ostream>

using nlohmann::json;

namespace Zuravvski
{
	struct Position
	{
		Position();
		Position(int x, int y, int angle, bool identified = false);

		int X;
		int Y;
		int Theta;
		bool Identified;

		static void to_json(json& j, const Position& position);
		friend std::ostream& operator<<(std::ostream& os, const Position& obj);

		static const char * TYPE;
	};
}

