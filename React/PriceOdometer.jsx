import React from "react";
import Odometer from "react-odometerjs";
import PropTypes from "prop-types";

const GKOdometer = ({ value }) => {
  return (
    <div>
      <Odometer value={value} />
    </div>
  );
};

GKOdometer.propTypes = {
  value: PropTypes.number.isRequired,
};
export default GKOdometer;
