#include "FrameProcessor.h"
#include <iostream>

namespace Zuravvski
{
	FrameProcessor::FrameProcessor(std::unique_ptr<IMovementTracking> movementTracking, std::unique_ptr<IIdentifier> identifier) :
		_movementTracking(move(movementTracking)), _identifier(move(identifier))
	{
	}

	void FrameProcessor::GetNotified(const cv::Mat& currentFrame)
	{
		auto robots = _movementTracking->FindRobots(currentFrame);
		_identifier->Identify(robots);
		std::vector<Position> positions;
		std::transform(robots.begin(), robots.end(), std::back_inserter(positions), [](RobotFrame robot) { return robot.GetPosition(); });
		
		for(auto& position : positions)
		{
			json j;
			Position::to_json(j, position);
			std::cout << j << std::endl;
		}
	}

	FrameProcessor::~FrameProcessor()
	{
	}
}