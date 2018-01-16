#pragma once
#include <vector>
#include "Position.h"

using namespace nlohmann;

struct Positions
{
	std::vector<Positions> positions;

	void to_json(json& j, const std::vector<Position>& positions)
	{
		j = positions;
	}

	void from_json(const json& json, const std::vector<Position>& positions)
	{
		// TODO
	}
};
