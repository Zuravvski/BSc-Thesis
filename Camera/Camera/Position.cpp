#include "Position.h"

namespace Zuravvski
{
	const char * Position::TYPE = "Infrastructure.Commands.Camera.Position, Infrastructure";

	Position::Position() : X(-1), Y(-1), Theta(-1), Identified(false)
	{
	}

	Position::Position(int x, int y, int angle, bool identified) : X(x), Y(y), Theta(angle), Identified(identified)
	{
	}

	std::ostream& operator<<(std::ostream& os, const Position& obj)
	{
		return os
			<< "x: " << obj.X
			<< " y: " << obj.Y
			<< " angle: " << obj.Theta
			<< " identified: " << std::boolalpha << obj.Identified;
	}

	void Position::to_json(json& j, const Position& position)
	{
		j = json{ { "$type", Position::TYPE },{ "x", position.X },
		{ "y", position.Y },{ "angle", position.Theta },
		{ "identified", position.Identified } };
	}
}
