using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seidltourplaner.Model
{
    internal class MainModel
    {
        public List<Vertex> m_allVertices { get; private set; }

        public MainModel()
        {
            m_allVertices = new List<Vertex>();
        }

        public void DefaultData()
        {
            // Anlegen der Knoten
            Vertex monkeys = new Vertex(47.80064507790365, 13.048103825465349, "Monkey cafe.bar");
            Vertex segabar = new Vertex(47.80004719908009, 13.046236505471862, "Segabar");
            Vertex steinlechners = new Vertex(47.798889396189196, 13.063847967838894, "Steinlechners");
            Vertex augustinerbraeu = new Vertex(47.80568, 13.03377, "Augustiner Bräu Kloster Mülln");
            Vertex watzmann = new Vertex(47.80101, 13.04686, "Watzmann Cultbar");
            Vertex partymaus = new Vertex(47.79197, 12.987, "Partymaus");
            Vertex schnaitlpub = new Vertex(47.80299, 13.04572, "Schnaitl Pub");
            Vertex rockhouse = new Vertex(47.80705, 13.05895, "Rockhouse Salzburg");
            Vertex weisse = new Vertex(47.80689, 13.05172, "Die Weisse");
            Vertex stiegelkeller = new Vertex(47.79645, 13.04801, "Stiegelkeller");
            Vertex urbankeller = new Vertex(47.80722, 13.06083, "Urbankeller Salzburg");
            Vertex cityalm = new Vertex(47.79979, 13.04678, "City Alm");
            Vertex omalleys = new Vertex(47.80012, 13.04607, "O'Malley's Irish Pub");
            Vertex flip = new Vertex(47.80114, 13.0389, "Flip");
            Vertex stiegelbrauwelt = new Vertex(47.79368, 13.02143, "Stiegl-Brauwelt");
            Vertex citybeats = new Vertex(47.80069, 13.04127, "City Beats");


            // Nachbarknoten
            // monkeys
            monkeys.AddNeighborVertix(segabar);
            monkeys.AddNeighborVertix(steinlechners);
            monkeys.AddNeighborVertix(watzmann);
            monkeys.AddNeighborVertix(omalleys);
            monkeys.AddNeighborVertix(cityalm);
            monkeys.AddNeighborVertix(citybeats);

            // segabar
            segabar.AddNeighborVertix(monkeys);
            segabar.AddNeighborVertix(watzmann);

            // Steinlechner
            steinlechners.AddNeighborVertix(urbankeller);
            steinlechners.AddNeighborVertix(monkeys);
            steinlechners.AddNeighborVertix(cityalm);
            steinlechners.AddNeighborVertix(stiegelkeller);

            // Augustiner Bräu
            augustinerbraeu.AddNeighborVertix(flip);
            augustinerbraeu.AddNeighborVertix(stiegelbrauwelt);
            augustinerbraeu.AddNeighborVertix(partymaus);
            augustinerbraeu.AddNeighborVertix(weisse);
            augustinerbraeu.AddNeighborVertix(schnaitlpub);

            // Watzmann
            watzmann.AddNeighborVertix(segabar);
            watzmann.AddNeighborVertix(monkeys);
            watzmann.AddNeighborVertix(omalleys);
            watzmann.AddNeighborVertix(schnaitlpub);
            watzmann.AddNeighborVertix(citybeats);

            // Partymaus
            partymaus.AddNeighborVertix(stiegelbrauwelt);
            partymaus.AddNeighborVertix(augustinerbraeu);
            partymaus.AddNeighborVertix(flip);

            // Schnaitl Pub
            schnaitlpub.AddNeighborVertix(weisse);
            schnaitlpub.AddNeighborVertix(rockhouse);
            schnaitlpub.AddNeighborVertix(watzmann);
            schnaitlpub.AddNeighborVertix(omalleys);
            schnaitlpub.AddNeighborVertix(citybeats);
            schnaitlpub.AddNeighborVertix(augustinerbraeu);

            // Rockhouse
            rockhouse.AddNeighborVertix(weisse);
            rockhouse.AddNeighborVertix(schnaitlpub);
            rockhouse.AddNeighborVertix(urbankeller);

            // Weisse
            weisse.AddNeighborVertix(rockhouse);
            weisse.AddNeighborVertix(schnaitlpub);
            weisse.AddNeighborVertix(augustinerbraeu);

            // Stieglkeller
            stiegelkeller.AddNeighborVertix(stiegelbrauwelt);
            stiegelkeller.AddNeighborVertix(steinlechners);
            stiegelkeller.AddNeighborVertix(cityalm);
            stiegelkeller.AddNeighborVertix(flip);
            stiegelkeller.AddNeighborVertix(citybeats);

            // Urbankeller
            urbankeller.AddNeighborVertix(rockhouse);
            urbankeller.AddNeighborVertix(steinlechners);

            // Cityalm
            cityalm.AddNeighborVertix(omalleys);
            cityalm.AddNeighborVertix(steinlechners);
            cityalm.AddNeighborVertix(stiegelkeller);
            cityalm.AddNeighborVertix(monkeys);

            // O'Mally's
            omalleys.AddNeighborVertix(watzmann);
            omalleys.AddNeighborVertix(monkeys);
            omalleys.AddNeighborVertix(cityalm);
            omalleys.AddNeighborVertix(citybeats);
            omalleys.AddNeighborVertix(schnaitlpub);


            // Flip
            flip.AddNeighborVertix(augustinerbraeu);
            flip.AddNeighborVertix(stiegelbrauwelt);
            flip.AddNeighborVertix(stiegelkeller);
            flip.AddNeighborVertix(citybeats);
            flip.AddNeighborVertix(partymaus);

            // Stiegl-Brauwelt
            stiegelbrauwelt.AddNeighborVertix(partymaus);
            stiegelbrauwelt.AddNeighborVertix(augustinerbraeu);
            stiegelbrauwelt.AddNeighborVertix(flip);
            stiegelbrauwelt.AddNeighborVertix(stiegelkeller);

            // City Beats
            citybeats.AddNeighborVertix(flip);
            citybeats.AddNeighborVertix(omalleys);
            citybeats.AddNeighborVertix(schnaitlpub);
            citybeats.AddNeighborVertix(watzmann);
            citybeats.AddNeighborVertix(monkeys);
            citybeats.AddNeighborVertix(stiegelkeller);

            // Liste aller Knoten generieren
            m_allVertices.Add(monkeys);
            m_allVertices.Add(segabar);
            m_allVertices.Add(steinlechners);
            m_allVertices.Add(augustinerbraeu);
            m_allVertices.Add(watzmann);
            m_allVertices.Add(partymaus);
            m_allVertices.Add(schnaitlpub);
            m_allVertices.Add(rockhouse);
            m_allVertices.Add(weisse);
            m_allVertices.Add(urbankeller);
            m_allVertices.Add(stiegelkeller);
            m_allVertices.Add(cityalm);
            m_allVertices.Add(omalleys);
            m_allVertices.Add(flip);
            m_allVertices.Add(stiegelbrauwelt);
            m_allVertices.Add(citybeats);
        }

    }
}
