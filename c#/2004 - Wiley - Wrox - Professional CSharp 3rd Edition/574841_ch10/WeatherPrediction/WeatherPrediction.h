// WeatherPrediction.h

/**
 * Predicts the weather using proven new-age
 * techniques given the current temperature
 * and the distance from Jupiter to Mars. If
 * these values are not provided, a guess is
 * still given but it's only 99% accurate.
 */
class WeatherPrediction
{
 public:
  virtual void setCurrentTempFahrenheit(int inTemp);
  virtual void setPositionOfJupiter(int inDistanceFromMars);

  /**
   * Gets the prediction for tomorrow's temperature.
   */
  virtual int getTomorrowTempFahrenheit();

  /**
   * Gets the probability of rain tomorrow. 1 means
   * definite rain. 0 means no chance of rain.
   */
  virtual double getChanceOfRain();

  /**
   * Displays the result to the user in this format:
   * Result: x.xx chance. Temp. xx
   */
  virtual void showResult();

 protected:
  int mCurrentTempFahrenheit;
  int mDistanceFromMars;
};
