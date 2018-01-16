#pragma once
#include "ICommand.h"
#include <json.hpp>

using namespace nlohmann;

namespace Zuravvski
{
	struct Identify : ICommand
	{
		unsigned int ID;
		unsigned int timeout;
	};

	void to_json(json& j, const Identify& identify)
	{
		j = json{ { "id", identify.ID },{ "timeout", identify.timeout } };
	}

	void from_json(const json& json, Identify& identify)
	{
		identify.ID = json.at("ID").get<unsigned int>();
		auto timeout = json.at("timeout").get<unsigned int>();
		identify.timeout = timeout == 0 ? 1 : timeout;
	}
}