// See https://aka.ms/new-console-template for more information

// #define TEST_POPULATION
// #define TEST_STRICT_MODULO
#define TEST_MOD_BY_PI

using SmartVectorDotNet.PoC;

#if TEST_POPULATION
PopulationPoC.Test();
#endif

#if TEST_STRICT_MODULO
StrictModuloPoC.Test();
#endif

#if TEST_MOD_BY_PI
ModByPiPoC.Test();
#endif
