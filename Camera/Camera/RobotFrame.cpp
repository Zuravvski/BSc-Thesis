#include "RobotFrame.h"

namespace Zuravvski
{
	RobotFrame::RobotFrame(const cv::Mat& frame, const Position& position) : _frame(frame), _position(position)
	{
	}

	cv::Mat RobotFrame::GetFrame() const
	{
		return _frame;
	}

	Position RobotFrame::GetPosition() const
	{
		return _position;
	}

	void RobotFrame::Identify(bool value)
	{
		_position.Identified = value;
	}
}