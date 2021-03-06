#pragma once
#include <memory>
#include "ICommand.h"
#include <json.hpp>

using namespace  nlohmann;

namespace Zuravvski
{
	struct RequestFactory
	{
		std::unique_ptr<ICommand> GetRequest(const json& json) const;
	};
}