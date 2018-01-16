#include "RequestFactory.h"
#include "Identify.h"

namespace Zuravvski
{
	std::unique_ptr<ICommand> RequestFactory::GetRequest(const json& json) const
	{
		const auto type = json.at("$type").get<std::string>();

		if (type == "identify")
		{
			return std::make_unique<Identify>();
		}
		return nullptr;
	}
}