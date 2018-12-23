/**
 * Implements the basic functionality of a watermelon.
 *
 * TODO: Implement updated algorithms!
 */
class Watermelon
{
 public:
  /**
   * @param initialSeeds The starting number of seeds
   */
  Watermelon(int initialSeeds);

  /**
   * Computes the seed ratio, using the Marigold
   * algorithm.
   *
   * @param slowCalc Whether or not to use long (slow) calculations
   * @return The marigold ratio
   */
  double calcSeedRatio(bool slowCalc);
}
